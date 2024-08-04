using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPF.UI.Services.Interfaces.Message;
using WPF.UI.Services.Interfaces.MessageBus;

namespace WPF.UI.Services.Realizations.MessageBus
{
    internal class MessageBusService : IMessageBus
    {
        #region Nested Classes

        private class Subscription<T, U> : IDisposable   
            where T : IMessage<U>
        {
            private readonly WeakReference<IMessageBus> m_bus;

            public Action<T> Handler { get; }

            public Subscription(IMessageBus bus, Action<T> action)
            {
                m_bus = new(bus);

                Handler = action;
            }

            // Unsubsription
            public void Dispose()
            {
                // GC has already cleaned that reference
                if (!m_bus.TryGetTarget(out var bus))
                    return;

                var Lock = bus.Lock;

                Lock.EnterWriteLock();
                var msg_type = typeof(T).Name;

                try
                {
                    if (!bus.Subscriptions.TryGetValue(msg_type, out var refs))
                        return;

                    var alived_refs = refs.Where(r => r.IsAlive).ToList();

                    WeakReference? curr_ref = null;

                    foreach (var r in alived_refs)
                    {
                        if (ReferenceEquals(r.Target, this))
                        {
                            curr_ref = r;
                            break;
                        }
                    }

                    if (curr_ref is null)
                        return;

                    alived_refs.Remove(curr_ref);

                    bus.Subscriptions[msg_type] = alived_refs;
                }
                finally
                {
                    Lock.ExitWriteLock();
                }

            }
        }

        #endregion

        private readonly Dictionary<string, IEnumerable<WeakReference>> m_Subscriptions;

        public Dictionary<string, IEnumerable<WeakReference>> Subscriptions
        {
            get => m_Subscriptions;
        }

        private readonly ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim();

        public ReaderWriterLockSlim Lock
        { 
            get => m_lock;
        }

        public MessageBusService()
        {
            m_Subscriptions = new Dictionary<string, IEnumerable<WeakReference>>();
        }

        public IDisposable RegisterHandler<T,U>(Action<T> handler)  
            where T : IMessage<U>
        {
            Subscription<T, U> subscription = new Subscription<T, U>(this, handler); 
            m_lock.EnterWriteLock();
            try
            {                
                var sub_weak_ref = new WeakReference(subscription);
                var msg_type = typeof(T).Name;

                m_Subscriptions[msg_type] = m_Subscriptions.TryGetValue(msg_type, out var subscriptions) ?
                    subscriptions.Append(sub_weak_ref) : new[] { sub_weak_ref };                
            }
            finally
            { 
                m_lock.ExitWriteLock();
            }

            return subscription;
        }

        private IEnumerable<Action<T>>? GetHandlersAccordingToMsgType<T, U>()
            where T : IMessage<U>
        {
            var handlers = new List<Action<T>>();
            var message_type = typeof(T).Name;
            var exist_die_refs = false;

            Lock.EnterReadLock();
            try
            {
                if (!Subscriptions.TryGetValue(message_type, out var refs))
                    return null;

                foreach (var @ref in refs)
                    if (@ref.Target is Subscription<T, U> { Handler: var handler })
                        handlers.Add(handler);
                    else
                        exist_die_refs = true;
            }
            finally
            {
                Lock.ExitReadLock();
            }

            if (!exist_die_refs) return handlers;

            Lock.EnterWriteLock();
            try
            {
                if (Subscriptions.TryGetValue(message_type, out var refs))
                    if (refs.Where(r => r.IsAlive).ToArray() is { Length: > 0 } new_refs)
                        Subscriptions[message_type] = new_refs;
                    else
                        Subscriptions.Remove(message_type);
            }
            finally
            {
                Lock.ExitWriteLock();
            }

            return handlers;
        }

        public void Send<T, U>(T message)
            where T : IMessage<U>
        {
            var handlers = GetHandlersAccordingToMsgType<T, U>();

            if (handlers is null)
                return;

            foreach (var h in handlers)
            {
                h.Invoke(message);
            }
        }
    }
}

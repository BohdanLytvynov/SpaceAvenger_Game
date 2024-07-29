using SpaceAvenger.Services.Interfaces.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceAvenger.Services.Realizations.MessageBus
{
    internal class MessageBusService : IMessageBus
    {
        private class Subscription<TMsg> : IDisposable
        {
            private readonly WeakReference<IMessageBus> m_bus;

            public Action<TMsg> Handler { get; }

            public Subscription(IMessageBus bus, Action<TMsg> action)
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
                var msg_type = typeof(TMsg);

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

        private readonly Dictionary<Type, IEnumerable<WeakReference>> m_Subscriptions = new();

        private readonly ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim();

        public ReaderWriterLockSlim Lock
        { 
            get => m_lock;
        }

        public Dictionary<Type, IEnumerable<WeakReference>> Subscriptions
        {
            get => m_Subscriptions;
        }

        public IDisposable RegisterHandler<T>(Action<T> handler)
        {
            Subscription<T> subscription = new Subscription<T>(this, handler); 
            m_lock.EnterWriteLock();
            try
            {                
                var sub_ref = new WeakReference(subscription);
                var msg_type = typeof(T);

                m_Subscriptions[msg_type] = m_Subscriptions.TryGetValue(msg_type, out var subscriptions) ?
                    subscriptions.Append(sub_ref) : new[] { sub_ref };                
            }
            finally
            { 
                m_lock.ExitWriteLock();
            }

            return subscription;
        }

        private IEnumerable<Action<T>> GetHandlersAccordingToType<T>()
        {
            var handlers = new List<Action<T>>();
            var message_type = typeof(T);
            var exist_die_refs = false;

            Lock.EnterReadLock();
            try
            {
                if (!Subscriptions.TryGetValue(message_type, out var refs))
                    return null;

                foreach (var @ref in refs)
                    if (@ref.Target is Subscription<T> { Handler: var handler })
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

        public void Send<T>(T message)
        {
            if (GetHandlersAccordingToType<T>() is not { } handlers)
                return;

            foreach (var h in handlers)
            {
                h.Invoke(message);
            }
        }
    }
}

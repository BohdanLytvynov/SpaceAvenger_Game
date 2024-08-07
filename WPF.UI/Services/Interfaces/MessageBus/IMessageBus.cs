using WPF.UI.Services.Interfaces.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF.UI.Services.Interfaces.MessageBus
{
    internal interface IMessageBus
    {
        IDisposable RegisterHandler<T>(Action<T> handler)
            where T : IMessage;           
            
        public ReaderWriterLockSlim Lock { get; }

        public Dictionary<Type, IEnumerable<WeakReference>> Subscriptions { get; }

        void Send<T>(T message)
            where T : IMessage;
            
    }
}

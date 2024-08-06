using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.Services.Interfaces.Message
{
    internal interface IGameMessage : IMessage
    {
    }

    internal interface IGameMessage<T> : IGameMessage
    {
        public T Args { get; }
    }
}

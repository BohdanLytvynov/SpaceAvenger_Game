using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAvenger.Services.Interfaces.Message
{   
    internal interface IMessage<T>
    {
        public T Content { get; }
    }
}

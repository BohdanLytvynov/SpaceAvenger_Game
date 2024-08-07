using WPF.UI.Services.Interfaces.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.Services.Realizations.Message
{
    internal abstract class Message<T> : IMessage<T>
    {
        private T m_content;

        public T Content
        { 
            get => m_content;
        }

        public Message(T content)
        {
            m_content = content; 
        }

        public override string ToString()
        {
            return $"{m_content}";
        }
    }
}

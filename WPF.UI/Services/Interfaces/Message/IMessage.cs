﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.Services.Interfaces.Message
{    
    internal interface IMessage<T> : IMessage
    {
        public T Content { get; }
    }

    internal interface IMessage
    { 
    
    }
}

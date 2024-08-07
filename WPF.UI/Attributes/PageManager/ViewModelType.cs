using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.Attributes.PageManager
{
    internal enum ViewModelUsage : byte
    { 
        Page = 1, Window
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal class ViewModelType : Attribute
    {
        public ViewModelUsage Usage { get; }

        public ViewModelType(ViewModelUsage usage)
        {
            Usage = usage;
        }
    }
}

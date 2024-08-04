using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.Attributes.PageManager
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ViewModelName : Attribute
    {
        public string Name { get; }

        public ViewModelName(string name)
        {
            Name = name;
        }
    }
}

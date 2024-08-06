using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.Interfaces.Loader
{
    public interface IResourceLoaderArgs<T>
    {
        public T Arg { get; set; }
    }
    
    public interface IResourceLoader<out TResource>        
    {
        TResource Load<T>(T resourceLoaderArgs);                       
    }
}

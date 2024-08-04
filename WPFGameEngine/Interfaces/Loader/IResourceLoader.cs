using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGameEngine.Interfaces.Loader
{
    public interface IResourceLoader<TResource>
    {
        TResource Load(params object[] args);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.GameObject.Base
{
    public interface IUpdateArgs
    {
    }

    public interface IUpdateArgs<T> : IUpdateArgs
    {
        public T Args { get; set; }
    }
}

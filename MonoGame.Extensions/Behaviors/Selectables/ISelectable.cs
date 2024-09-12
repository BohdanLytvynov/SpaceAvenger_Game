using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Behaviors.Selectables
{
    public interface ISelectable
    {
        void Select();

        void Deselect();

        public bool Selected { get; }
    }
}

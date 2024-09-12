using MonoGame.Extensions.Physics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.UI.Enums.ModuleTypes;

namespace WPF.UI.MonoGameCore.Modules
{
    internal interface IModule : IRigidBodyObject
    {
        public ModuleType Type { get; }
    }
}

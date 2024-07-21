using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ViewModelBaseLibDotNetCore.VM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string PropName)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string PropName = null )
        {
            if (field.Equals(value))
            {
                return false;
            }
            else
            {
                field = value;

                OnPropertyChanged(PropName);

                return true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


namespace ViewModelBaseLibDotNetCore.VM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Dispatcher m_dispatcher;

        public virtual Dispatcher Dispatcher { set => m_dispatcher = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string PropName)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string PropName = null)
        {            
            if (field == null)
            { 
                throw new ArgumentNullException(string.Format("Property: {0}", PropName));
            }

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

        protected virtual void QueueWorkToDispatcher(Action work)
        {
            if (m_dispatcher is null)
                throw new Exception("Dispatcher is not initialized!");

            m_dispatcher?.Invoke(work);
        }
    }
}

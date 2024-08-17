using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLibDotNetCore.Commands;

namespace WPF.UI.CommonCommands
{
    public class ExitCommand : Command
    {
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            App.Current.MainWindow.Close();
        }
    }
}

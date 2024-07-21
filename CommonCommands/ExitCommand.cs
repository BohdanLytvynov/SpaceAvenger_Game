using ViewModelBaseLibDotNetCore.Commands;

namespace CommonCommands
{
    public class ExitCommand : Command
    {       
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            Environment.Exit(0);
        }

    }
}
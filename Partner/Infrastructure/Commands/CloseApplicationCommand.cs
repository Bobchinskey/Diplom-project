using Partner.Infrastructure.Commands.Base;
using System.Windows;

namespace Partner.Infrastructure.Commands
{
    class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parametr) => true;

        public override void Execute(object parametr) => Application.Current.Shutdown();

    }
}

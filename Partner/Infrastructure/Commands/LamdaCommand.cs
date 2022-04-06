using Partner.Infrastructure.Commands.Base;
using System;

namespace Partner.Infrastructure.Commands
{
    internal class LamdaCommand : Command
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LamdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }
        public override bool CanExecute(object parametr) => _CanExecute?.Invoke(parametr) ?? true;

        public override void Execute(object parametr) => _Execute(parametr);
    }
}

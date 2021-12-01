using BankUI.Interfaces;
using Prism.Commands;

namespace BankUI.Core.Commands
{
    public class ApplicationCommand : IApplicationCommands
    {
        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}

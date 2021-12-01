using BankUI.Interfaces;
using Prism.Commands;

namespace BankUI.Commands
{
    public class ApplicationCommand : IApplicationCommands
    {
        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}

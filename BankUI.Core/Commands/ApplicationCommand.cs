using BankUI.Interfaces;
using Prism.Commands;

namespace BankUI.Core.Commands
{
    /// <summary>
    /// Общие для всего приложения команды
    /// </summary>
    public class ApplicationCommand : IApplicationCommands
    {
        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}

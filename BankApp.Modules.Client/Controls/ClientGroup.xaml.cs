using BankUI.Core.Common;
using BankUI.Interfaces;
using System.Windows.Controls;

namespace BankApp.Modules.Client.Controls
{
    /// <summary>
    /// Interaction logic for ClientGroup
    /// </summary>
    public partial class ClientGroup : UserControl
    {
        public ClientGroup(IApplicationCommands _applicationCommands)
        {
            InitializeComponent();
            _dataTree.Loaded += (s, e) =>
            {
                _applicationCommands.NavigateCommand.Execute(FolderParameters.DefaultNavigationPath);
            };
        }
    }
}

using System.Windows.Controls;

namespace BankApp.Modules.Client.Views
{
    /// <summary>
    /// Interaction logic for AccountView
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
            _cmbxAccountType.Loaded += (s, e) =>
            {
                var _tems = _cmbxAccountType.Items;
                _cmbxAccountType.SelectedItem = _tems[0];
            };
        }
    }
}

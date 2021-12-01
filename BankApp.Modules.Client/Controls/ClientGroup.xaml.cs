using System.Windows.Controls;

namespace BankApp.Modules.Client.Controls
{
    /// <summary>
    /// Interaction logic for ClientGroup
    /// </summary>
    public partial class ClientGroup : UserControl
    {
        public ClientGroup()
        {
            InitializeComponent();
            _dataTree.Loaded += (s, e) =>
            {
                
                //var parentNode = _dataTree.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
                
                
                //parentNode.IsSelected = true;
                //var tt = parentNode.Items[0];


            };
        }
    }
}

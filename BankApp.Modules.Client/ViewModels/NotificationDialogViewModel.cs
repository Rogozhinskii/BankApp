using BankUI.Core.Common;
using Prism.Services.Dialogs;

namespace BankApp.Modules.Client.ViewModels
{
    /// <summary>
    /// ViewModel информационного диалогового окна 
    /// </summary>
    public class NotificationDialogViewModel : ErrorDialogViewModel
    {
        public NotificationDialogViewModel() { }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(CommonTypesPrism.NotificationMessage);
        }
    }
}

using BankUI.Core.Common;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace BankApp.Modules.Client.ViewModels
{
    public class ErrorDialogViewModel : DialogViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public override string Title => "Error occured";

        public ErrorDialogViewModel() { }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

        void ExecuteCloseCommand()
        {
            DialogResult dialogResult = new DialogResult(ButtonResult.OK);
            RaiseRequestClose(dialogResult);
        }

        public override bool CanCloseDialog()
        {
            return true;
        }
        
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(CommonTypesPrism.ErrorMessage);
        }
    }
}

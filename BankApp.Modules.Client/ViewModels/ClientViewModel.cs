using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Modules.Client.ViewModels
{
    class ClientViewModel:BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ClientViewModel()
        {
            Message = "nigga view";
        }
    }
}

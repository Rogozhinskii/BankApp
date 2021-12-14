using BankApp.Modules.Client.Controls;
using BankApp.Modules.Client.ViewModels;
using BankApp.Modules.Client.Views;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Services;
using BankUI.Core.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.Modules.Client
{
    public class ClientModule : IModule
    {
        private readonly IRegionManager regionManager;

        public ClientModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {            
            regionManager.RegisterViewWithRegion(CommonTypesPrism.cClientGroup, typeof(ClientGroup));            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<ClientGroup, ClientGroupViewModel>();
            containerRegistry.RegisterForNavigation<ClientList, ClientListViewModel>();
            containerRegistry.RegisterSingleton<IClientService, ClientService>();
            containerRegistry.RegisterDialog<AccountView, AccountViewModel>();
            containerRegistry.RegisterDialog<ErrorDialog, ErrorDialogViewModel>();
            containerRegistry.RegisterDialog<TransactionView, TransactionViewModel>();
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
            containerRegistry.RegisterDialog<AccountInfoView, AccountInfoViewModel>();
            containerRegistry.RegisterDialog<AccountConsolidationView, AccountConsolidationViewModel>();
            containerRegistry.RegisterSingleton<IAccountService<IAccount>, AccountService<IAccount>>();            
        }
    }
}
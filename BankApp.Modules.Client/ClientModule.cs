using BankApp.Modules.Client.Controls;
using BankApp.Modules.Client.ViewModels;
using BankApp.Modules.Client.Views;
using BankLibrary.Common;
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
        }
    }
}
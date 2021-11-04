using BankApp.Modules.Client.Views;
using BankLibrary.Common;
using Prism.Ioc;
using Prism.Modularity;
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
            regionManager.RegisterViewWithRegion(CommonTypesPrism.cBankClient, typeof(ClientView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
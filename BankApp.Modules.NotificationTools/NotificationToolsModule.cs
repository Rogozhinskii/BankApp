using BankApp.Modules.NotificationTools.ViewModels;
using BankApp.Modules.NotificationTools.Views;
using BankUI.Core.Common;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BankApp.Modules.NotificationTools
{
    public class NotificationToolsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NotificationToolsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(CommonTypesPrism.StatusBarRegion, typeof(StatusBar));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<LogForm,LogFormViewModel>();
        }
    }
}
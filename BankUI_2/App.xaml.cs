using BankApp.Modules.Client;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Commands;
using BankUI.Interfaces;
using BankUI.Views;
using DryIoc;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using System.Configuration;
using System.Windows;

namespace BankUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger logger;
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            logger = LogManager.GetCurrentClassLogger();
            containerRegistry.RegisterSingleton<IApplicationCommands,ApplicationCommand>();
            const string path = "ConnectionString";
            string connectionString = ConfigurationManager.AppSettings.Get(path);
            containerRegistry.RegisterInstance(typeof(ILogger), logger);
            var repositoryManager = new RepositoryManager(logger, connectionString);
            containerRegistry.RegisterInstance<IRepositoryManager>(repositoryManager);            
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            if (moduleCatalog != null)
            {
                moduleCatalog.AddModule<ClientModule>();
            }
        }
    }
}

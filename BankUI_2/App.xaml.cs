using BankApp.Modules.Client;
using BankApp.Modules.NotificationTools;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Commands;
using BankUI.Core.Services;
using BankUI.Core.Services.Interfaces;
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

        /// <summary>
        /// Регистрацция сервисов
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            logger = LogManager.GetCurrentClassLogger();
            containerRegistry.RegisterSingleton<IApplicationCommands,ApplicationCommand>();
            const string path = "ConnectionString";
            string connectionString = ConfigurationManager.AppSettings.Get(path);
            containerRegistry.RegisterInstance(typeof(ILogger), logger);
            var repositoryManager = new RepositoryManager(logger, connectionString);
            containerRegistry.RegisterInstance<IRepositoryManager>(repositoryManager);
            containerRegistry.RegisterSingleton<ILogService, LogService>();
            
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            if (moduleCatalog != null)
            {
                moduleCatalog.AddModule<ClientModule>(); //Добавляем модуль
                moduleCatalog.AddModule<NotificationToolsModule>();
            }
        }
    }
}

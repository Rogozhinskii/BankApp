using BankApp.Modules.Client;
using BankApp.Modules.NotificationTools;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using BankLibrary.Model.Exceptions;
using BankUI.Core.Commands;
using BankUI.Core.Common;
using BankUI.Core.Services;
using BankUI.Core.Services.Interfaces;
using BankUI.Interfaces;
using BankUI.Views;
using DryIoc;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace BankUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger logger;
        private IDialogService _dialogService;
        protected override Window CreateShell()
        {
            _dialogService = Container.Resolve<IDialogService>() ?? throw new NullReferenceException(nameof(IDialogService));
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// все не обработанные исключения будут идти сюда. хотя стратегия не самая лучшая...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add(CommonTypesPrism.ErrorMessage, e.Exception.Message);
            _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
            e.Handled = true;
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
            containerRegistry.RegisterSingleton<ISaveService, SaveService>();
            
            
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

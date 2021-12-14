using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace BankApp.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// ViewModel статус бара
    /// </summary>
    public class StatusBarViewModel : BindableBase
    {        
        private readonly ISaveService _saveService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        public StatusBarViewModel(ISaveService saveService,IDialogService dialogService,IEventAggregator eventAgregator)
        {
            _saveService = saveService;
            _dialogService = dialogService;            
            _eventAggregator = eventAgregator;
            _eventAggregator.GetEvent<LongOperationEvent>().Subscribe(LongOperationOccure);
        }

        private void LongOperationOccure(Visibility obj)
        {            
            ProgressBarVisibility = obj;
        }

        private Visibility _progressBarVisibility=Visibility.Hidden;
        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { SetProperty(ref _progressBarVisibility, value); }
        }
        private DelegateCommand _showLogCommand;

        /// <summary>
        /// Вызывает диалоговое окно логов
        /// </summary>
        public DelegateCommand ShowLogCommand =>
            _showLogCommand ??=_showLogCommand = new DelegateCommand(ExecuteShowLogCommand);

        void ExecuteShowLogCommand()
        {
            _dialogService.Show(CommonTypesPrism.LogForm, null, result =>
              {

              });
        }

        private DelegateCommand _saveDataCommand;

        /// <summary>
        /// Сохраняет проведенный изменения
        /// </summary>
        public DelegateCommand SaveDataCommand =>
            _saveDataCommand ??=_saveDataCommand = new DelegateCommand(ExecuteSaveDataCommand);

        void ExecuteSaveDataCommand()
        {
            var result = _saveService.SaveData();
            LogRecord logRecord;
            if (result){
                logRecord = new LogRecord{
                    LogRecordLevel = LogRecordLevel.Info,
                    Message = $"{DateTime.Now}-->Данные сохранены"
                };
            }
            else{
                logRecord = new LogRecord{
                    LogRecordLevel = LogRecordLevel.Error,
                    Message = $"{DateTime.Now}-->При сохранении произошла ошибка"
                };
            }
            _eventAggregator.GetEvent<LogEvent>().Publish(logRecord);
            ShowDialog(result);
        }

        /// <summary>
        /// Вызывает диалоговое окна в зависимости от результата сохранения данных
        /// </summary>
        /// <param name="result"></param>
        private void ShowDialog(bool result)
        {
            var dialogParameters = new DialogParameters();
            if (result){
                string notifyMessage = "Данные сохранены";
                dialogParameters.Add(CommonTypesPrism.NotificationMessage, notifyMessage);
                _dialogService.ShowDialog(CommonTypesPrism.NotificationDialog, dialogParameters, result => { });
            }
            else{
                string errorMessage = "Сохранение не выполнено";
                dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
            }
        }

    }
}

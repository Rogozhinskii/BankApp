﻿using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common;
using BankUI.Core.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Modules.NotificationTools.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {        
        private readonly ISaveService _saveService;
        private readonly IDialogService _dialogService;

        public StatusBarViewModel(ISaveService saveService,IDialogService dialogService)
        {
            _saveService = saveService;
            _dialogService = dialogService;
        }

        private DelegateCommand _showLogCommand;
        public DelegateCommand ShowLogCommand =>
            _showLogCommand ?? (_showLogCommand = new DelegateCommand(ExecuteShowLogCommand));

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
            _saveDataCommand ?? (_saveDataCommand = new DelegateCommand(ExecuteSaveDataCommand));

        void ExecuteSaveDataCommand()
        {
            var result = _saveService.SaveData();
            ShowDialog(result);
        }

        /// <summary>
        /// Вызывает диалоговое окна в зависимости от результата сохранения данных
        /// </summary>
        /// <param name="result"></param>
        private void ShowDialog(bool result)
        {
            var dialogParameters = new DialogParameters();
            if (result)
            {
                string notifyMessage = "Данные сохранены";
                dialogParameters.Add(CommonTypesPrism.NotificationMessage, notifyMessage);
                _dialogService.ShowDialog(CommonTypesPrism.NotificationDialog, dialogParameters, result => { });

            }
            else
            {
                string errorMessage = "Сохранение не выполнено";
                dialogParameters.Add(CommonTypesPrism.ErrorMessage, errorMessage);
                _dialogService.ShowDialog(CommonTypesPrism.ErrorDialog, dialogParameters, result => { });
            }
        }

    }
}

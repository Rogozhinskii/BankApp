using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services
{
    public class SaveService : ISaveService
    {
        private readonly IClientService _clientService;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEventAggregator _eventAggregator;

        public SaveService(IClientService clientService, IRepositoryManager repositoryManager, IEventAggregator eventAggregator)
        {
            _clientService = clientService;
            _repositoryManager = repositoryManager;
            _eventAggregator = eventAggregator;
        }
        public bool SaveData()
        {         
            try
            {
                var result=_repositoryManager.CommitChanges(_clientService.GetAllClients());
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
                return result;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }

        }
    }
}

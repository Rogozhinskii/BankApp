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
    /// <summary>
    /// Для сохранения изменений данных о клиентах
    /// </summary>
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

using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Services.Interfaces;
using Prism.Events;
using System;

namespace BankUI.Core.Services
{
    /// <summary>
    /// Для сохранения изменений данных о клиентах
    /// </summary>
    public class SaveService : ISaveService
    {
        private readonly IClientService _clientService;
        private readonly IRepositoryManager _repositoryManager;
        

        public SaveService(IClientService clientService, IRepositoryManager repositoryManager, IEventAggregator eventAggregator)
        {
            _clientService = clientService;
            _repositoryManager = repositoryManager;            
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

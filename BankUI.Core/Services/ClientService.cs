using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI.Core.Services
{
    /// <summary>
    /// Для взаимодействия UI и хранилищем 
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRepositoryManager _repositoryManager;
        private List<IStorableDoc> _clients;
        private List<IClient> _regularClientItems = new List<IClient>();
        private List<IClient> _specialClientItems = new List<IClient>();

        public ClientService(IEventAggregator eventAggregator,IRepositoryManager repositoryManager)
        {
            _eventAggregator = eventAggregator;
            _repositoryManager = repositoryManager;
            _clients = _repositoryManager.ReadStorableDataAsList().ToList();
            EnrichClietnLists();           
        }

        /// <summary>
        /// Распределяет элементы коллекции _client по соответсвующим коллекциям в зависимости от типа хранимых клиентов
        /// </summary>
        private void EnrichClietnLists(){            
            foreach (var item in _clients){
                if(item is IClient client){
                    if (client.ClientType == ClientType.Regular){
                        _regularClientItems.Add(client);
                    }
                    if (client.ClientType == ClientType.Special){
                        _specialClientItems.Add(client);
                    }
                }
            }
        }

        public bool SaveNewAccount(Guid ownerId,IAccount account)
        {            
            var found = _clients.FirstOrDefault(x => x.Id == ownerId);
            if (found != null && found is IClient client){
                client.Accounts.Add(account);
                _eventAggregator.GetEvent<LogEvent>().Publish(
                    new LogRecord
                    {
                        LogRecordLevel = LogRecordLevel.Info,
                        Message = $"Счет номер: {account.Id} создан. Баланс: {account.Balance}. Владелец {client.Name} {client.Surname} номер:{found.Id}"
                    }
                    );
                return true;
            }
            return false;
        }

        public IList<IClient> GetRegularClients(){
            return _regularClientItems;
        }

        public IList<IClient> GetSpecialClients(){
            return _specialClientItems;
        }

        public bool SaveData(){       //todo убрать в бар     
            try{
                _repositoryManager.CommitChanges(_clients);
                _eventAggregator.GetEvent<LogEvent>().Publish(new LogRecord
                {
                    LogRecordLevel=LogRecordLevel.Info,
                    Message="Данные сохранены"
                });
                return true;
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            
        }

        
    }
}

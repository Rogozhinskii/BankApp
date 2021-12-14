using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankUI.Core.Services
{
    /// <summary>
    /// Для взаимодействия UI и хранилищем 
    /// </summary>
    public class ClientService : IClientService
    {
        
        private readonly IRepositoryManager _repositoryManager;
        private List<IStorableDoc> _clients;
        private List<IClient> _regularClientItems = new();
        private List<IClient> _specialClientItems = new();

        public ClientService(IRepositoryManager repositoryManager)
        {           
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

        public bool SaveNewAccount(Guid ownerId,IAccount account){            
            var found = _clients.FirstOrDefault(x => x.Id == ownerId);
            if (found != null && found is IClient client){
                client.Accounts.Add(account);               
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

       
        public IList<IStorableDoc> GetAllClients()
        {
            return _clients;
        }
    }
}

using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUI.Core.Services
{
    /// <summary>
    /// Для взаимодействия UI и хранилищем 
    /// </summary>
    public class ClientService : IClientService
    {
        
        
        private List<IStorableDoc> _clients;
        private List<IClient> _regularClientItems = new();
        private List<IClient> _specialClientItems = new();
        private readonly IRepositoryManager _repositoryManager;

        public ClientService(IRepositoryManager repositoryManager)
        {           
            _repositoryManager = repositoryManager;
            Task.Factory.StartNew(async () => {
                _clients = (List<IStorableDoc>)await _repositoryManager.ReadStorableDataAsListAsync();
                EnrichClietnLists();
            });
            
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

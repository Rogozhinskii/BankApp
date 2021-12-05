using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using BankUI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepositoryManager _repositoryManager;
        private List<IStorableDoc> _clients;
        private List<IClient> _regularClientItems = new List<IClient>();
        private List<IClient> _specialClientItems = new List<IClient>();

        public ClientService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _clients = _repositoryManager.ReadClientDataAsList().ToList();
            EnrichClietnLists();           
        }

        private void EnrichClietnLists()
        {
            
            foreach (var item in _clients)
            {
                if(item is IClient client)
                {
                    if (client.ClientType == ClientType.Regular)
                    {
                        _regularClientItems.Add(client);
                    }
                    if (client.ClientType == ClientType.Special)
                    {
                        _specialClientItems.Add(client);
                    }
                }
            }
        }

        public void CommitChanges(IEnumerable<IStorableDoc> clients)
        {
            _repositoryManager.CommitChanges(clients);
        }

        public bool SaveNewAccount(Guid ownerId,IAccount account)
        {            
            var found = _clients.FirstOrDefault(x => x.Id == ownerId);
            if (found != null && found is IClient client)
            {
                client.Accounts.Add(account);
                return true;
            }
            return false;
        }

        public IList<IAccount> GetAccounts(IClient storableDoc)
        {
            return storableDoc.Accounts;
        }

        public IList<IClient> GetRegularClients()
        {
            return _regularClientItems;
        }

        public IList<IClient> GetSpecialClients()
        {
            return _specialClientItems;
        }

        //public IList<IClient> GetAllClients()
        //{
        //    //return (IList<IClient>)_clients.Cast<IClient>();
        //}
    }
}

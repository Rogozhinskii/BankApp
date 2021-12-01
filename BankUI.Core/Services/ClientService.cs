using BankLibrary.Model;
using BankLibrary.Model.ClientModel;
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
        private List<IStorableDoc> _regularClientItems = new List<IStorableDoc>();
        private List<IStorableDoc> _specialClientItems = new List<IStorableDoc>();

        public ClientService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _clients = _repositoryManager.ReadClientDataAsList().ToList();
            _regularClientItems = _clients.Where(x => ((ClientBase)x).ClientType == ClientType.Regular).ToList();
            _specialClientItems = _clients.Where(x => ((ClientBase)x).ClientType == ClientType.Special).ToList();
            //_regularClientItems = _clients.Where(x => ((RegularClient)x).ClientType == ClientType.Regular);
            //_specialClientItems = (List<SpecialClient>)_clients.Where(x => ((SpecialClient)x).ClientType == ClientType.Special);

        }
        public IList<IStorableDoc> GetRegularClients()
        {
            return _regularClientItems;
        }

        public IList<IStorableDoc> GetSpecialClients()
        {
            return _specialClientItems;
        }
    }
}

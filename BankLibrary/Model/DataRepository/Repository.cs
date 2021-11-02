using BankLibrary.Model.DataRepository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BankLibrary.Model.DataRepository
{
    internal class Repository : IRepository<IStorableDoc>
    {
        private readonly IRepositoryManager repositoryManager;
        public Repository(IRepositoryManager repManager)
        {
            repositoryManager = repManager ?? throw new ArgumentNullException(nameof(repManager));
        }
        public void AddToStorage(IStorableDoc obj)
        {
            if(obj is Client client)
            {
                string json = JsonConvert.SerializeObject(client);
            }
        }

        public IEnumerable<IStorableDoc> Deserialize()
        {
            throw new NotImplementedException();
        }

        public IStorableDoc GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Serialize(IEnumerable<IStorableDoc> enumarableObjects)
        {
            throw new NotImplementedException();
        }
    }
}

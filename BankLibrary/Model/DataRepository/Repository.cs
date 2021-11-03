using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankLibrary.Model.DataRepository
{
    internal class Repository : IRepository<IStorableDoc>
    {
        private readonly IRepositoryManager repositoryManager;

        public string FilePath => throw new NotImplementedException();

        public Repository(IRepositoryManager repManager)
        {
            repositoryManager = repManager ?? throw new ArgumentNullException(nameof(repManager));
        }


        public void AddToStorage(IStorableDoc obj)
        {            
            
        }

        public IEnumerable<IStorableDoc> Deserialize(){
            JsonSerializerSettings settings = new JsonSerializerSettings{
                TypeNameHandling = TypeNameHandling.Auto               
            };
            string json = File.ReadAllText(repositoryManager.ConnectionString);
            return JsonConvert.DeserializeObject<IEnumerable<IStorableDoc>>(json, settings); 
            
        }

        public IStorableDoc GetClientById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Serialize(IEnumerable<IStorableDoc> enumerableObjects)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings{ 
                TypeNameHandling = TypeNameHandling.Auto, 
                Formatting = Formatting.Indented 
            };           
            string json= JsonConvert.SerializeObject(enumerableObjects, settings);           
            File.WriteAllText(repositoryManager.ConnectionString,json);


        }
    }
}

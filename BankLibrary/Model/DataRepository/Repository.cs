using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
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


        public IEnumerable<IStorableDoc> Deserialize(){
           
            string json = File.ReadAllText(repositoryManager.ConnectionString);
            JArray arr = JArray.Parse(json);
            List<IStorableDoc> docs = new List<IStorableDoc>(); 
            foreach (var item in arr){
                var doc = DeserializeItem(item);
                docs.Add(doc);               
            }
            return docs;
        }


        private IStorableDoc DeserializeItem(JToken item)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings{
                TypeNameHandling = TypeNameHandling.Auto
            };
            var clientType = Enum.Parse(typeof(ClientType), item["ClientType"].ToString());
            IStorableDoc client = clientType switch
            {
                ClientType.Regular => new RegularClient(id:new Guid(item["Id"].ToString()),
                                                        name:item["Name"].ToString(),
                                                        surname:item["Surname"].ToString()),
                ClientType.Special => new SpecialClient(id: new Guid(item["Id"].ToString()),
                                                        name: item["Name"].ToString(),
                                                        surname: item["Surname"].ToString()),
                _ => null
            };

            ((IClient)client).Accounts = new List<IAccount>(JsonConvert.DeserializeObject<List<IAccount>>(item["Accounts"].ToString(),
                                                                                                    settings));
            return client;
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

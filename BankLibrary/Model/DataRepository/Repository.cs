using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
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
            JArray arr = JArray.Parse(json);
            List<IStorableDoc> docs = new List<IStorableDoc>(); 
            foreach (var item in arr){                
                if (item["ClientType"].ToString() == ((int)ClientType.Regular).ToString()){
                    RegularClient client = new RegularClient{
                        Name = item["Name"].ToString(),
                        Surname = item["Surname"].ToString(),
                        Id = new Guid(item["Id"].ToString()),
                        Accounts = new List<IAccount>(JsonConvert.DeserializeObject<List<IAccount>>(item["Accounts"].ToString(), 
                                                                                                    settings))
                    };
                    docs.Add(client);
                }
                else{
                    if(item["ClientType"].ToString() == ((int)ClientType.Special).ToString()){
                        SpecialClient client = new SpecialClient
                        {
                            Name = item["Name"].ToString(),
                            Surname = item["Surname"].ToString(),
                            Id = new Guid(item["Id"].ToString()),
                            Accounts = new List<IAccount>(JsonConvert.DeserializeObject<List<IAccount>>(item["Accounts"].ToString(),
                                                                                                    settings))
                        };
                        docs.Add(client);
                    }
                    else { continue; }
                }
            }
            return docs;
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

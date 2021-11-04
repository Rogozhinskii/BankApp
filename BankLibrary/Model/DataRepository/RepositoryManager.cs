using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.DataRepository
{
    
    public class RepositoryManager:IRepositoryManager
    {
        private readonly string repositoryPath;
        private IRepository<IStorableDoc> repository;
        public string ConnectionString => repositoryPath;

        private readonly ILogger logger;

        IEnumerable<IStorableDoc> clientsList;

        public RepositoryManager(ILogger log,string path)
        {
            logger = log ?? throw new ArgumentNullException($"{nameof(log)} логгер не инициирован");
            repositoryPath = path ?? throw new NullReferenceException($"{nameof(path)} пустая строка подключения");
            repository = new Repository(this);
            //clientsList = ReadClientDataAsList();
            clientsList = new List<IStorableDoc>();
        }

        

        private bool SimpleValidatePath(string path)
        {
            if (File.Exists(path))
                return true;
            else
                throw new ArgumentException("файл не существует");            
        }

        public void AddToStarge(IStorableDoc doc){           
            repository.AddToStorage(doc);
        }

        public IEnumerable<IStorableDoc> ReadClientDataAsList(){
            
            logger?.Info("Попытка чтения репозитория");
            try{
                return repository.Deserialize();

            }
            catch (UnauthorizedAccessException ex){
                logger?.Error($"Ошибка доступа к файлу. {ex.Message}");
            }
            return Enumerable.Empty<IStorableDoc>();
        }

        public IAccount GetAccountById(Guid guid)=>
                clientsList.SelectMany(r => (r as Client).Accounts)
                           .Where(x => x.Id == guid)
                           .SingleOrDefault();

        public bool CommitChanges()
        {
            return CommitChanges(clientsList);
        }
        public bool CommitChanges(IEnumerable<IStorableDoc> storableDocs)
        {
            bool flag;
            if (SimpleValidatePath(repositoryPath))
            {
                //todo добавить catch и лог              
                try
                {
                    repository.Serialize(storableDocs);
                    flag = true;
                    return flag;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return default;
        }
    }
}

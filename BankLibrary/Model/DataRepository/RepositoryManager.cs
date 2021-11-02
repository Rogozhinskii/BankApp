using BankLibrary.Model.DataRepository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
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

        public RepositoryManager(ILogger log,string path)
        {
            logger = log ?? throw new ArgumentNullException($"{nameof(log)} логгер не инициирован");
            repositoryPath = path ?? throw new NullReferenceException($"{nameof(path)} пустая строка подключения");
            repository = new Repository(this);
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


    }
}

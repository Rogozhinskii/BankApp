using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.DataRepository.Interfaces
{
    /// <summary>
    /// Интерфейс для взаимодействия с хранилищем
    /// </summary>
    public interface IRepositoryManager
    {
        /// <summary>
        /// Путь к хранилищу
        /// </summary>
        public string ConnectionString { get;}

        /// <summary>
        /// Извлекает данные из хранилища, возвращает, как перечисление
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStorableDoc> ReadStorableDataAsList();        
        
        /// <summary>
        /// Сохраняет изменение и записывает их в хранилище
        /// </summary>
        /// <returns></returns>
        bool CommitChanges();
        /// <summary>
        /// Сохраняет коллекцию измененых объектов
        /// </summary>
        /// <param name="storableDocs"></param>
        /// <returns></returns>
        bool CommitChanges(IEnumerable<IStorableDoc> storableDocs);

    }
}

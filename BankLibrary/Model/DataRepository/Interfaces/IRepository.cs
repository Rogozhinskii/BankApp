using BankLibrary.Model.DataRepository.Interfaces;
using System.Collections.Generic;

namespace BankLibrary.Model.DataRepository
{
    /// <summary>
    /// Хранилка объектов
    /// </summary>
    /// <typeparam name="T">IStorableDoc</typeparam>
    interface IRepository<T> where T:IStorableDoc
    {
        /// <summary>
        /// Сериализует хранимые объекты
        /// </summary>
        /// <param name="enumarableObjects"></param>
        void Serialize(IEnumerable<T> enumarableObjects);

        /// <summary>
        /// Десериализует хранимые объекты
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Deserialize();
        
    }
}

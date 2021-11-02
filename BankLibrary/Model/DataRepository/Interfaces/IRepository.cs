using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;

namespace BankLibrary.Model.DataRepository
{
    interface IRepository<T>
    {        
        T GetClientById(Guid id);
        void AddToStorage(T obj);
        void Serialize(IEnumerable<T> enumarableObjects);
        IEnumerable<T> Deserialize();
        void AddToStorage(IStorableDoc doc);
    }
}

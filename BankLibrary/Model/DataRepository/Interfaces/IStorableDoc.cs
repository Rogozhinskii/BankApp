using System;

namespace BankLibrary.Model.DataRepository.Interfaces
{
    /// <summary>
    /// Хранимы объект 
    /// </summary>
    public interface IStorableDoc
    {
        /// <summary>
        /// Уникальный номер объекта
        /// </summary>
        public Guid Id { get;}
    }
}

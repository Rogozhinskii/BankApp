using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

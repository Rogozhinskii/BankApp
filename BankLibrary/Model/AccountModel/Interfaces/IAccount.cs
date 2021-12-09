using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface IAccount
    {
        /// <summary>
        /// Уникальный номер счета
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Количество средств на счету
        /// </summary>
        public float Balance { get; }
        /// <summary>
        /// Тип счета
        /// </summary>
        AccountType AccountType { get; }
        /// <summary>
        /// Тип владельца счета
        /// </summary>
        ClientType ClientType { get; set; }
      
        /// <summary>
        /// возвращает true, когда со счета могут быть списаны средства, иначе false
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        bool CanReduceBalance(float count);

        /// <summary>
        /// уменьшает количество средств на счету на количество равное count
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        bool ReduceBalance(float count);

        /// <summary>
        /// увеличивает количество средств на счету на количество равное count
        /// </summary>
        /// <param name="count"></param>
        bool IncreaseBalance(float count);
    }
}

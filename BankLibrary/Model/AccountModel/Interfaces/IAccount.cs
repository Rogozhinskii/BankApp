using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface IAccount
    {
        public Guid Id { get; set; }
        public float Balance { get; }
        AccountType AccountType { get; }



        bool CanReduceBalance(float count);
        bool ReduceBalance(float count);
        void IncreaseBalance(float count);
    }
}

using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface IAccount
    {
        public Guid Id { get; }
        public float Balance { get; }
        AccountType AccountType { get; }
        public Client Owner { get;}

        bool CanReduceBalance(float count);
        bool ReduceBalance(float count);

        void IncreaseBalance(float count);
    }
}

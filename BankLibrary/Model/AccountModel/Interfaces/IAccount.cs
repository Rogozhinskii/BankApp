using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface IAccount
    {
        public Guid Id { get; set; }
        public float Balance { get; }
        AccountType AccountType { get; }

        ClientType ClientType { get; set; }
      

        bool CanReduceBalance(float count);
        bool ReduceBalance(float count);
        void IncreaseBalance(float count);
    }
}

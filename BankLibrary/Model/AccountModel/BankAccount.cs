using BankLibrary.Model;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.AccountModel
{
    public class BankAccount:IAccount
    {
        private Guid id;
        private float balance;
        private Client owner;
        private AccountType accountType;
        public Guid Id => id;

        public float Balance => balance;

        public AccountType AccountType => accountType;

        public Client Owner => owner;

        

        public BankAccount(float balance=default)
        {
            id = Guid.NewGuid();
            this.balance = balance;
        }

        public BankAccount(AccountType accountType):this()
        {
            this.accountType = accountType;
        }

        public IStorableDoc Clone()
        {
            throw new NotImplementedException();
        }
    }
}

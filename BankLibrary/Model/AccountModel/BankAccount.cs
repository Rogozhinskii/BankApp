using BankLibrary.Model;
using System;

namespace BankLibrary.AccountModel
{
    internal class BankAccount
    {
        public Guid Id { get; private set; }

        public Client Owner { get; set; }
        public float Balance { get; private set; }

        public BankAccount(float balance=default){
            Id = Guid.NewGuid();
            Balance = balance;
        }
        
       
    }
}

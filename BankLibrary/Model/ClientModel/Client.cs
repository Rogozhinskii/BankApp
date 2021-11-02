using BankLibrary.AccountModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;

namespace BankLibrary.Model
{
    public abstract class Client:IStorableDoc
    {
        private Guid id;
        public Guid Id { get => id; set => id = value; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<BankAccount> Accounts { get; set; }
       

        public Client(string name,string surname)
        {
            id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Accounts = new List<BankAccount>();
        }
      
        public void RegisterAccount(BankAccount account)
        {
            Accounts.Add(account);
        }
        public bool OpenAccount()
        {
            throw new NotImplementedException();
        }

        public bool CloseAccount()
        {
            throw new NotImplementedException();
        }

        public IStorableDoc Clone()
        {
            throw new NotImplementedException();
        }
    }
}

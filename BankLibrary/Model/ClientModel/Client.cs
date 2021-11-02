using BankLibrary.AccountModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;

namespace BankLibrary.Model
{
    public abstract class Client:IStorableDoc
    {
        private Guid guid;
        public Guid Guid { get => guid; set => guid = value; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<BankAccount> Accounts { get;}
       

        public Client(string name,string surname)
        {
            guid = Guid.NewGuid();
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

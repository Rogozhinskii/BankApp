using BankLibrary.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
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

        public ClientType ClientType { get; set; }

        public List<IAccount> Accounts { get; set; }
       

        //public Client(string name,string surname,ClientType type)
        //{
            
        //    Name = name;
        //    Surname = surname;
        //    ClientType = type;
        //    Accounts = new List<IAccount>();
        //}
      
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

        public void SetId(Guid id) =>
            this.id = id;
    }
}

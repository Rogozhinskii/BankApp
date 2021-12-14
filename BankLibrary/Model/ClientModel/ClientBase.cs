using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;

namespace BankLibrary.Model
{
    public abstract class ClientBase:IStorableDoc,IClient
    {
        private readonly Guid _id;
        public Guid Id => _id;
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public ClientType ClientType { get; set; }

        public List<IAccount> Accounts { get; set; } = new List<IAccount>();

        public ClientBase(Guid id, string name, string surname,ClientType clientType){
            _id = id;
            Name = name;
            Surname = surname;
            ClientType = clientType;
        }

       
       
    }
}

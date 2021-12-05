using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.ClientModel.Interfaces
{
    public interface IClient
    {
        public List<IAccount> Accounts { get; set; }
        public ClientType ClientType { get; set; }
        //bool OpenAccount();
        //bool CloseAccount();
    }
}

using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services.Interfaces
{
    public interface IClientService
    {
        //IList<IClient> GetAllClients();
        IList<IClient> GetRegularClients();
        IList<IClient> GetSpecialClients();

        IList<IAccount> GetAccounts(IClient storableDoc);
        bool SaveNewAccount(Guid ownerId,IAccount account);
        bool SaveData();
       
    }
}

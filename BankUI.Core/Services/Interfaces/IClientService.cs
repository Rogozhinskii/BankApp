using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services.Interfaces
{
    public interface IClientService
    {
        IList<IStorableDoc> GetRegularClients();
        IList<IStorableDoc> GetSpecialClients();

        IList<IAccount> GetAccounts(IStorableDoc storableDoc);

    }
}

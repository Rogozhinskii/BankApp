using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.DataRepository.Interfaces
{
    public interface IRepositoryManager
    {
        public string ConnectionString { get;}
        IEnumerable<IStorableDoc> ReadClientDataAsList();
        void AddToStarge(IStorableDoc doc);
        IAccount GetAccountById(Guid guid);

        bool CommitChanges();

    }
}

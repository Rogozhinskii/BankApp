using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.DataRepository.Interfaces
{
    public interface IStorableDoc
    {
        public Guid Id { get; set; }

        public List<IAccount> Accounts { get; set; }
        IStorableDoc Clone();
    }
}

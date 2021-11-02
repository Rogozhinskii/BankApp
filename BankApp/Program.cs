using System;
using BankLibrary;
using BankLibrary.Model;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using NLog;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using System.Linq;
using System.Collections.Generic;
using BankLibrary.AccountModel;
using System.IO;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            string path = @$"{Directory.GetCurrentDirectory()}\Storage.json";
            IRepositoryManager repositoryManager = new RepositoryManager(logger,path);
            IAccountManager accountManager = new AccountManager();
           
            var result= accountManager.CreateNewAccount(AccountType.NonDeposit);

            var tt = Test(accountManager);
            var flag=repositoryManager.CommitChanges(tt);

        }

        static List<RegularClient> Test(IAccountManager accountManager)
        {
            return Enumerable.Range(1, 10)
                .Select(x => new RegularClient($"{x}", $"{++x}")
                {
                    Accounts = Enumerable.Range(1, 10).Select(i => (BankAccount)accountManager.CreateNewAccount(AccountType.NonDeposit)).ToList()
                })
                .ToList();
        }
    }
}
 
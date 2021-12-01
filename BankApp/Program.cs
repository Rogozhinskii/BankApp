using System;
using BankLibrary.Model;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using NLog;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.DataRepository.Data;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            string path = @$"{Directory.GetCurrentDirectory()}\Storage.json";
            IRepositoryManager repositoryManager = new RepositoryManager(logger,path);
            IAccountManager<DepositAccount> accountManager = new AccountManager<DepositAccount>(repositoryManager);

            //var rere = repositoryManager.ReadClientDataAsList();
            var tt = Test(accountManager);
            var flag = repositoryManager.CommitChanges(tt);

            //var resrt = accountManager.SendMoney(new Guid("c7ae549c-3b71-4d67-b930-1e538ff542ec"), new Guid("42af5150-2b41-466f-8c34-283ee48c47cc"),50);
            //var res=repositoryManager.CommitChanges(tt);


        }

        static List<ClientBase> Test(IAccountManager<DepositAccount> accountManager)
        {
            var tt = Enumerable.Range(1, 10)
                .Select(x => new RegularClient
                {
                    Name = RandomData.GetRandomName(),
                    Surname = RandomData.GetRandomSurname(),
                    Id = Guid.NewGuid(),
                    ClientType = ClientType.Regular,
                    Accounts = Enumerable.Range(1, 10).Select(i => (IAccount)accountManager.CreateNewAccount(0.0f)).ToList()
                }).ToList();
            var pp = Enumerable.Range(1, 10)
                .Select(x => new SpecialClient
                {
                    Name = RandomData.GetRandomName(),
                    Surname = RandomData.GetRandomSurname(),
                    Id = Guid.NewGuid(),
                    ClientType = ClientType.Special,
                    Accounts = Enumerable.Range(1, 10).Select(i => (IAccount)accountManager.CreateNewAccount(0.0f)).ToList()
                }).ToList();

            var list = new List<ClientBase>(tt);
            list.AddRange(pp);
            return list;
        }
    }
}
 
using System;
using BankLibrary;
using BankLibrary.Model;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using NLog;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            string path = @"C:\Users\rogoz\OneDrive\Рабочий стол\DESKTOP-7BDPRJG.txt";
            IRepositoryManager repositoryManager = new RepositoryManager(logger,path);
            IAccountManager accountManager = new AccountManager();
            RegularClient client = new("Roman","RRR");
            repositoryManager.AddToStarge(client);
            accountManager.CreateNewAccount(AccountType.NonDeposit)
        }
    }
}
 
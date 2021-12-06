using BankLibrary.Model;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Data;
using BankLibrary.Model.DataRepository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace BankAppTests
{
    /// <summary>
    /// Песочница для экспериментов
    /// </summary>
    [TestClass]
    public class ScrapBook
    {
       [TestMethod]
        public void RepositoryTest()
        {
            var logger = LogManager.GetCurrentClassLogger();
            const string path = @"C:\Users\rogoz\source\repos\BankApp\BankUI_2\bin\Debug\netcoreapp3.1\Storage.json";
            RepositoryManager repositoryManager = new RepositoryManager(logger,path);
            var list = repositoryManager.ReadClientDataAsList();
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void SerializeTest()
        {
            const string path = @"C:\Users\rogoz\source\repos\BankApp\BankUI_2\bin\Debug\netcoreapp3.1\Storage.json";
           
            var clients = Enumerable.Range(1,10).Select(i=>new RegularClient(Guid.NewGuid(),RandomData.GetRandomName(),RandomData.GetRandomSurname())
            {   
                Name = RandomData.GetRandomName(),
                Surname = RandomData.GetRandomSurname()                
            });

            var specClients = Enumerable.Range(1, 10).Select(i => new SpecialClient(Guid.NewGuid(), RandomData.GetRandomName(), RandomData.GetRandomSurname())
            {
                Name = RandomData.GetRandomName(),
                Surname = RandomData.GetRandomSurname()
            });

            var storable = new List<IStorableDoc>();
            storable.AddRange(clients);
            storable.AddRange(specClients);

            var rnd = new Random();
            foreach (var client in storable)
            {
                var acc = new List<IAccount>();
                var depAcc = Enumerable.Range(1, 5).Select(i => new DepositAccount(Guid.NewGuid(), 10f * i) {ClientType=((IClient)client).ClientType,Term=rnd.Next(12,36) });
                var savAcc = Enumerable.Range(1, 5).Select(i => new SavingAccount(Guid.NewGuid(), 10f * i) { ClientType = ((IClient)client).ClientType });
                acc.AddRange(depAcc);
                acc.AddRange(savAcc);
                ((IClient)client).Accounts = new List<IAccount>(acc);
            }
            var logger = LogManager.GetCurrentClassLogger();
            RepositoryManager repositoryManager = new RepositoryManager(logger, path);
            repositoryManager.CommitChanges(storable);


        }
    }
}

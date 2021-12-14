using BankLibrary.Model;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Data;
using BankLibrary.Model.DataRepository.Interfaces;
using BankLibrary.Model.Exceptions;
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
        public void ExcceprionTest()
        {
            var from = new SavingAccount(Guid.NewGuid(), 15);
            var to = new SavingAccount(Guid.NewGuid(), 55);

            from.Transaction(500);

            try
            {
                from.Transaction(to, 20);
            }catch(NotEnoughBalanceException ex)
            {
                _ = ex.Message;
            }

            
            
        }


       [TestMethod]
        public void RepositoryTest()
        {
            var logger = LogManager.GetCurrentClassLogger();           
            RepositoryManager repositoryManager = new(logger);
            var list = repositoryManager.ReadStorableDataAsList();
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void SerializeTest()
        {
            var clients = Enumerable.Range(1,1_000_000).Select(i=>new RegularClient(Guid.NewGuid(),RandomData.GetRandomName(),RandomData.GetRandomSurname())
            {   
                Name = RandomData.GetRandomName(),
                Surname = RandomData.GetRandomSurname()                
            });

            var specClients = Enumerable.Range(1, 1_000_000).Select(i => new SpecialClient(Guid.NewGuid(), RandomData.GetRandomName(), RandomData.GetRandomSurname())
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
            RepositoryManager repositoryManager = new(logger);
            repositoryManager.CommitChanges(storable);


        }
    }
}

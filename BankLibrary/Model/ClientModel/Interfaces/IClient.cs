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
        /// <summary>
        /// Имя клента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Коллекция счетов
        /// </summary>
        public List<IAccount> Accounts { get; set; }

        /// <summary>
        /// Тип клиента
        /// </summary>
        public ClientType ClientType { get; set; }
        
    }
}

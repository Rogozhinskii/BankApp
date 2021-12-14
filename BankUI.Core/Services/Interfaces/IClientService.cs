using BankLibrary.Model;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.ClientModel.Interfaces;
using BankLibrary.Model.DataRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Core.Services.Interfaces
{
    /// <summary>
    /// Интерфейс доступа к хранимым данным о клиента
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Возвращает коллекцию клиентов, у которых ClientType==ClientType.Regular
        /// </summary>
        /// <returns></returns>
        IList<IClient> GetRegularClients();


        Task<IList<IClient>> GetRegularClientsAsync();

        /// <summary>
        /// Возвращает коллекцию клиентов, у которых ClientType==ClientType.Special
        /// </summary>
        /// <returns></returns>
        IList<IClient> GetSpecialClients();

        Task<IList<IClient>> GetSpecialClientsAsync();

        /// <summary>
        /// Добавляет новых счет к коллекции счетов клиента
        /// </summary>
        /// <param name="ownerId">Id владельца счета</param>
        /// <param name="account">сам счет</param>
        /// <returns></returns>
        bool SaveNewAccount(Guid ownerId,IAccount account);       

        /// <summary>
        /// Возвращает коллекцию всех пользователей
        /// </summary>
        /// <returns></returns>
        IList<IStorableDoc> GetAllClients();

    }
}

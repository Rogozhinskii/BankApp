﻿using System.Threading.Tasks;

namespace BankUI.Core.Services.Interfaces
{
    public interface ISaveService
    {
        /// <summary>
        /// Сохраняет изменения
        /// </summary>
        /// <returns></returns>
        bool SaveData();

        Task<bool> SaveDataAsync();

    }
}

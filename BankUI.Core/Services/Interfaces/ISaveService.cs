using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services.Interfaces
{
    public interface ISaveService
    {
        /// <summary>
        /// Сохраняет изменения
        /// </summary>
        /// <returns></returns>
        bool SaveData();

    }
}

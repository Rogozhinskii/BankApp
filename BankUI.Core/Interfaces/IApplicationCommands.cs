using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Interfaces
{
    /// <summary>
    /// Интерфейс общих для приложения сомманд
    /// </summary>
    public interface IApplicationCommands
    {
        /// <summary>
        /// Выполняет переключение между view при изменении контекста навигации
        /// </summary>
        CompositeCommand NavigateCommand { get; }
    }
}

using BankUI.Core.Common.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services.Interfaces
{
    public interface ILogService
    {
        LogRecord LastMessage { get; }
        List<LogRecord> GetAllRecord();

        public event EventHandler UpdateLogEvent;
    }
}

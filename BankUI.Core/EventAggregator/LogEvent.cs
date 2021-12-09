using BankUI.Core.Common.Log;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.EventAggregator
{
    /// <summary>
    /// Событие записи в лог
    /// </summary>
    public class LogEvent: PubSubEvent<LogRecord>
    {
    }
}

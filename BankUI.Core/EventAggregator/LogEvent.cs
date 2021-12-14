using BankUI.Core.Common.Log;
using Prism.Events;

namespace BankUI.Core.EventAggregator
{
    /// <summary>
    /// Событие записи в лог
    /// </summary>
    public class LogEvent: PubSubEvent<LogRecord>
    {
    }
}

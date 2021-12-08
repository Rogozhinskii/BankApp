using BankUI.Core.Common.Log;
using Prism.Events;

namespace BankUI.Core.EventAggregator
{
    public class NewAccountOpenedEvent:PubSubEvent<LogRecord>
    {
    }
}

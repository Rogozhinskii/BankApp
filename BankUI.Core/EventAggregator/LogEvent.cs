using BankUI.Core.Common.Log;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.EventAggregator
{
    public class LogEvent: PubSubEvent<LogRecord>
    {
    }
}

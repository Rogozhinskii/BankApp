using Prism.Events;
using System.Windows;

namespace BankUI.Core.EventAggregator
{
    public class LongOperationEvent:PubSubEvent<Visibility>
    {
    }
}

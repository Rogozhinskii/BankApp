using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Interfaces
{
    public interface IApplicationCommands
    {
        CompositeCommand NavigateCommand { get; }
    }
}

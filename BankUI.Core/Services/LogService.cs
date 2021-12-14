using BankUI.Core.Common.Log;
using BankUI.Core.EventAggregator;
using BankUI.Core.Services.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankUI.Core.Services
{
    public class LogService : ILogService
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly List<LogRecord> _records = new();
        public event EventHandler UpdateLogEvent;

        public LogRecord LastMessage
        {
            get
            {
                LogRecord lastRecord = null;
                if(_records!=null && _records.Any())
                {
                   lastRecord=_records.Last();
                }
                return lastRecord;
            }
        }

        public LogService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LogEvent>().Subscribe(AddNewLogRecord);
            
        }

        private void AddNewLogRecord(LogRecord record)
        {
            if (record != null)
            {
                _records.Add(record);
                UpdateLogEvent?.Invoke(this, new EventArgs());
            }
        }

        public List<LogRecord> GetAllRecord() =>
            _records;
        
    }
}

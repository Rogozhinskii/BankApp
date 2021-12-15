namespace BankUI.Core.Common.Log
{
    /// <summary>
    /// Запись в лог форме
    /// </summary>
    public class LogRecord
    {
        /// <summary>
        /// сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Некий тип сообщения в логе
        /// </summary>
        public LogRecordLevel LogRecordLevel { get; set; }

        public LogRecord()
        {
            LogRecordLevel = LogRecordLevel.None;
            Message = string.Empty;
        }
    }
}

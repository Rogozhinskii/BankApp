using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using BankUI.Core.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankApp.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// ViewModel окна логов
    /// </summary>
    public class LogFormViewModel : DialogViewModelBase
    {
        /// <summary>
        /// сервис хранения и доступа к логам логов
        /// </summary>
        private readonly ILogService _logService;
        public override string Title => "Логи";
        public LogFormViewModel(ILogService logService)
        {
            _logService = logService;
            _logService.UpdateLogEvent += (s, e) =>
            {
                LogRecord newRecord = _logService.LastMessage;
                if (newRecord != null)
                {
                    _log.Add(newRecord);
                }

            };
        }

        private ObservableCollection<LogRecord> _log = new();
        /// <summary>
        /// Записи логов
        /// </summary>
        public ObservableCollection<LogRecord> Log
        {
            get
            {
                if (!_log.Any()){
                    _log.AddRange(_logService.GetAllRecord());
                }
                return _log;
            }
            
        }
    }
}
 
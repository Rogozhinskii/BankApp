namespace BankUI.Core.Common
{
    /// <summary>
    /// Общие типы и константы для навигации и dialogService
    /// </summary>
    public class CommonTypesPrism
    {
        #region Regions
        public const string ContentRegion = "ContentRegion"; 
        public const string cClientGroup = "ClientGroup";
        public const string StatusBarRegion = "StatusBarRegion";
        
        #endregion

        #region Dialog Constants
        public const string AccountView = "AccountView";
        public const string ParameterNewAccount = "NewAccount";
        public const string ParameterOwner = "Owner";
        public const string ParameterAccounts = "Accounts";
        public const string ErrorDialog = "ErrorDialog";
        public const string ErrorMessage = "ErrorMessage";
        public const string TransactionView = "TransactionView";
        public const string NotificationDialog = "NotificationDialog";
        public const string NotificationMessage = "NotificationMessage";
        public const string SelectedAccount = "SelectedAccount";
        public const string AccountInfoView = "AccountInfoView";
        public const string LogForm = "LogForm";
        public const string AccountConsolidationView = "AccountConsolidationView";

        #endregion

        #region other constants
        public const float zeroValue = 0f;
        #endregion
    }
}

namespace BankUI.Core.Common
{
    /// <summary>
    /// Константы для реализации навигации по модулям 
    /// </summary>
    public class FolderParameters
    {
        public const string FolderKey = "Folder";
        public const string Regular = "Regular";
        public const string Special = "Special";
        public static string DefaultNavigationPath = $"ClientList?{FolderKey}={Regular}";

    }
}

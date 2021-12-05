namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface ITransactionManager<in T>
    {
        void SendMoneyToAccount(T accaunt, float sum);
        void SendMoneyToAccount(T fromAccaunt, T toAccaunt, float sum);
    }
}

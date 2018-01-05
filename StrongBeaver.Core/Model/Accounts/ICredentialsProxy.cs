namespace StrongBeaver.Core.Model.Accounts
{
    public interface ICredentialsProxy
    {
        void StoreCredential(ICredential credential);

        ICredential GetCredential(AccountTypeEnum accountType);

        void DeleteCredential(AccountTypeEnum accountType);
    }
}
using System.Threading.Tasks;
using StrongBeaver.Core.Model.Accounts;

namespace StrongBeaver.Core.Services.Accounts
{
    public interface IAccountsService : IService
    {
        bool UseProxy { get; set; }

        Task<IAccount> GetAccountAsync(AccountTypeEnum accountType);

        Task<IAccount> GetOrCreateAccountAsync(AccountTypeEnum accountType);

        Task<ICredential> GetValidCredentialAsync(AccountTypeEnum accountType);

        Task<ICredential> GetValidCredentialAsync(IAccount account);

        Task AddRightsToAccountAsync(IAccount account, params string[] rights);

        bool HasAccountRights(IAccount account, params string[] rights);

        string[] GetMissingRights(IAccount account, params string[] rights);
    }
}
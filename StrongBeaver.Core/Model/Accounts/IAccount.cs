using System.Collections.Generic;

namespace StrongBeaver.Core.Model.Accounts
{
    public interface IAccount
    {
        AccountTypeEnum AccountType { get; }

        string RefreshToken { get; }

        ISet<string> Rights { get; }
    }
}
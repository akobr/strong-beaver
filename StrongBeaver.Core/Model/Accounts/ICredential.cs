using System;

namespace StrongBeaver.Core.Model.Accounts
{
    public interface ICredential
    {
        IAccount Account { get; }

        string Token { get; }

        DateTimeOffset ExpirationTime { get; }
    }
}
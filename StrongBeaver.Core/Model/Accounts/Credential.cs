using System;

namespace StrongBeaver.Core.Model.Accounts
{
    public class Credential : ICredential
    {
        public Credential(IAccount account, string token, DateTimeOffset expirationTime)
        {
            Account = account;
            Token = token;
            ExpirationTime = expirationTime;
        }

        public Credential(IAccount account, string token, int expiresInSeconds)
            : this(account, token, DateTimeOffset.UtcNow.AddSeconds(expiresInSeconds))
        {
            // No operation
        }

        public Credential(IAccount account, string token, TimeSpan expiresIn)
            : this(account, token, DateTimeOffset.UtcNow.Add(expiresIn))
        {
            // No operation
        }

        public IAccount Account { get;  }

        public string Token { get; }

        public DateTimeOffset ExpirationTime { get; }
    }
}
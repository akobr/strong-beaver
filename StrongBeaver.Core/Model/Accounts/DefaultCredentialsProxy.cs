using System;
using System.Collections.Generic;

namespace StrongBeaver.Core.Model.Accounts
{
    public class DefaultCredentialsProxy : BaseStore, ICredentialsProxy
    {
        private readonly IDictionary<AccountTypeEnum, ICredential> credentials;

        public DefaultCredentialsProxy()
        {
            credentials = new Dictionary<AccountTypeEnum, ICredential>();
        }

        public void StoreCredential(ICredential credential)
        {
            if (credential.ExpirationTime <= DateTimeOffset.UtcNow.AddSeconds(2))
            {
                return;
            }

            AccountTypeEnum key = credential.Account.AccountType;

            if (credentials.ContainsKey(key))
            {
                credentials[key] = credential;
                return;
            }

            credentials.Add(key, credential);
        }

        public ICredential GetCredential(AccountTypeEnum accountType)
        {
            if (!credentials.ContainsKey(accountType))
            {
                return null;
            }

            ICredential credential = credentials[accountType];

            if (credential.ExpirationTime <= DateTimeOffset.UtcNow.AddSeconds(2))
            {
                credentials.Remove(accountType);
                return null;
            }

            return credential;
        }

        public void DeleteCredential(AccountTypeEnum accountType)
        {
            credentials.Remove(accountType);
        }
    }
}
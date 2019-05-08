using System;
using System.Collections.Generic;
using Contracts;

namespace Entities.Models
{
    public interface IAccountRepository: IRepositoryBase<Account>
    {
        IEnumerable<Account> AccountsByOwner(Guid ownerId);
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(Guid accountId);
        void CreateAccount(Account o);
        void UpdateAccount(Account dbAccount, Account account);
        void DeleteAccount(Account account);
        
    }
}
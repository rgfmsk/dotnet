using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }
        
        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll().OrderBy(ac => ac.DateCreated).Reverse().ToList();
        }

        public Account GetAccountById(Guid accountId)
        {
            return FindByCondition(a => a.Id.Equals(accountId)).DefaultIfEmpty(new Account()).FirstOrDefault();
        }

        public void CreateAccount(Account o)
        {
            o.Id = Guid.NewGuid();
            Create(o);
        }

        public void UpdateAccount(Account dbAccount, Account account)
        {
            dbAccount.Map(account);
            Update(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }
    }
}
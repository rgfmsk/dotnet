using Entities.Models;

namespace Entities.Extensions
{
    public static class AccountExtensions
    {
        public static void Map(this Account dbAccount, Account account)
        {
            dbAccount.AccountType = account.AccountType;
            dbAccount.OwnerId = account.OwnerId;
        }
    }
}
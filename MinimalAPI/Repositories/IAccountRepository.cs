

namespace MinimalAPI.Repositories;

    internal interface IAccountRepository
    {
     public  Task<Account?> GetAccount(string accountNumber);
      public  Task<bool> AddAccount(Account account);

     public Task<IEnumerable<Account>> GetAllAccounts();

    public Task<bool> DeleteAccount(string accountNumber);

    public Task<Account> UpdateAccount(Account account);


    }


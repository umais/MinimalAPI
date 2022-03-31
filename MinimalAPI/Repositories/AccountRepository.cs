namespace MinimalAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public readonly DatabaseConfig _config;
        public AccountRepository(DatabaseConfig config)
        {
            _config = config;
        }
        public async Task<bool> AddAccount(Account account)
        {
            using var connection = new SqliteConnection(_config.DatabaseName);

            var result= await connection.ExecuteAsync("INSERT INTO Accounts (AccountNumber, AccountName,AccountType,AccountUser,AccountPassword)" + "VALUES (@accountNumber,@accountName,@accountType,@accountUser, @accountPassword);", account);

            return result > 0;
        }

        public async Task<Account?> GetAccount(string accountNumber)
        {
            using var connection= new SqliteConnection(_config.DatabaseName);
           
            var result = await connection.QueryAsync<Account>("SELECT rowid AS Id, AccountNumber,AccountName,AccountType,AccountUser,AccountPassword from Accounts Where AccountNumber=" + accountNumber + " ;");

            return result.Any() ? result.FirstOrDefault() : null;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            using var connection = new SqliteConnection(_config.DatabaseName);

            var result = await connection.QueryAsync<Account>("SELECT rowid AS Id, AccountNumber,AccountName,AccountType,AccountUser,AccountPassword from Accounts ;");

            return result;
        }

        Task<bool> IAccountRepository.DeleteAccount(string accountNumber)
        {
            throw new NotImplementedException();
        }

        Task<Account> IAccountRepository.UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}

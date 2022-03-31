

namespace MinimalAPI.DataLibrary;

    public class SQLiteDatabaseBootstrap:IDatabaseBootstrap
    {
        private readonly DatabaseConfig _config;

        public SQLiteDatabaseBootstrap(DatabaseConfig config)
        {
            _config = config;
        }

        public void SetUp()
        {
        string AccountSchema = "Create Table Accounts (" +
         "AccountNumber VARCHAR(100) NOT NULL," +
         "AccountName VARCHAR(1000) NULL," +
         "AccountType VARCHAR(255) NULL," +
         "AccountUser varchar(255) NULL," +
         "AccountPassword varchar(255) NULL);";
        string AccountTable = "Accounts";

        CreateTable(AccountTable,AccountSchema);

    }

       private int CreateTable(string Tablename, string TableSchema)
    {
        using var connection = new SqliteConnection(_config.DatabaseName);
        var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = '"+Tablename+"';");
        var tableName = table.FirstOrDefault();
        if (!string.IsNullOrEmpty(tableName) && tableName == Tablename)
            return 0;

        var result =connection.Execute(TableSchema);
        return result;
    }
    }


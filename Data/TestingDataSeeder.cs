using Microsoft.Data.Sqlite;

namespace ContactManager.Data
{
    public class TestingDataSeeder
    {
        private readonly string _connectionString;

        public TestingDataSeeder(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task EnsureDatabaseSeededAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();

            // 1. Create table if it doesn't exist
            command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Contacts (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FirstName TEXT NOT NULL,
            LastName TEXT NOT NULL,
            Phone TEXT NOT NULL,
            Email TEXT,
            City TEXT
        );";
            await command.ExecuteNonQueryAsync();

            // 2. Check if any data exists
            command.CommandText = "SELECT COUNT(*) FROM Contacts;";
            var count = (long)await command.ExecuteScalarAsync();

            // 3. Insert sample if empty
            if (count == 0)
            {
                command.CommandText = @"
            INSERT INTO Contacts (FirstName, LastName, Phone, Email, City)
            VALUES 
                ('Alice', 'Smith', '+123456789', 'alice@example.com', 'New York'),
                ('Bob', 'Johnson', '+987654321', 'bob@example.com', 'London'),
                ('Carol', 'Lee', '+444555666', NULL, 'Prague');";
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}

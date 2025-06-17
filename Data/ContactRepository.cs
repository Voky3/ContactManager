using ContactManager.Models;
using Microsoft.Data.Sqlite;

namespace ContactManager.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task CreateAsync(Contact contact)
        {
            using var connection = await CreateConnectionAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO Contacts (FirstName, LastName, Phone, Email, City)
            VALUES (@FirstName, @LastName, @Phone, @Email, @City);";

            command.Parameters.AddWithValue("@FirstName", contact.FirstName);
            command.Parameters.AddWithValue("@LastName", contact.LastName);
            command.Parameters.AddWithValue("@Phone", contact.Phone);
            command.Parameters.AddWithValue("@Email", (object?)contact.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@City", (object?)contact.City ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }        

        public async Task<IEnumerable<Contact>> GetAllAsync(string? filter, string? sortBy)
        {
            var contacts = new List<Contact>();

            using var connection = await CreateConnectionAsync();

            var command = connection.CreateCommand();
            var sql = "SELECT * FROM Contacts";

            // Add filter
            if (!string.IsNullOrWhiteSpace(filter))
            {
                sql += @"
                 WHERE FirstName LIKE @filter 
                    OR LastName LIKE @filter 
                    OR City LIKE @filter";

                command.Parameters.AddWithValue("@filter", $"%{filter}%");
            }

            // Add sorting — allow only whitelisted columns
            var validSortFields = new[] { "FirstName", "LastName", "City" };
            if (!string.IsNullOrWhiteSpace(sortBy) && validSortFields.Contains(sortBy))
                sql += $" ORDER BY {sortBy}";

            command.CommandText = sql;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                contacts.Add(ReadContact(reader));

            return contacts;
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            using var connection = await CreateConnectionAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Contacts WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return ReadContact(reader);

            return null;
        }

        public async Task UpdateAsync(Contact contact)
        {
            using var connection = await CreateConnectionAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
            UPDATE Contacts
            SET FirstName = @FirstName,
                LastName = @LastName,
                Phone = @Phone,
                Email = @Email,
                City = @City
            WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", contact.Id);
            command.Parameters.AddWithValue("@FirstName", contact.FirstName);
            command.Parameters.AddWithValue("@LastName", contact.LastName);
            command.Parameters.AddWithValue("@Phone", contact.Phone);
            command.Parameters.AddWithValue("@Email", (object?)contact.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@City", (object?)contact.City ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = await CreateConnectionAsync();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Contacts WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();
        }

        private async Task<SqliteConnection> CreateConnectionAsync()
        {
            var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private Contact ReadContact(SqliteDataReader reader)
        {
            return new Contact
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Phone = reader.GetString(3),
                Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                City = reader.IsDBNull(5) ? null : reader.GetString(5)
            };
        }
    }
}

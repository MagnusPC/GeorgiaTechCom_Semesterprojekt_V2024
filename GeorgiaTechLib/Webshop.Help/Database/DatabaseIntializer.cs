using Npgsql;

public class DatabaseService
{
    private string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PGconnectionString");
    }


    public static void InitializeDatabase(string connectionString)
    {
        using var connection = new NpgsqlConnection(connectionString);

        CreateDatabase(connection);

        connection.ChangeDatabase("psuwebshop");
        
        CreateTable(connection);
        SeedDatabase(connection);

        CreateTable(newConnection);
        SeedDatabase(newConnection);

    }

    public static void CreateDatabase(NpgsqlConnection connection)
    {
        using (connection)
        {
            connection.Open();

            try
            {
                // Attempt to create the database
                string createDbCommand = "CREATE DATABASE psusearch";

                using (var command = new NpgsqlCommand(createDbCommand, connection))
                {
                    // If the database already exists, this will throw an error
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database 'psusearch' created successfully.");
                }
            }
            catch (PostgresException ex)
            {
                // If database already exists, catch the exception and skip creation
                if (ex.SqlState == "42P04") // Error code for "database already exists"
                {
                    Console.WriteLine("Database 'psusearch' already exists.");
                }
                else
                {
                    // If any other error occurs, throw it
                    throw;
                }
            }
        }
    }

    public static void CreateTable(NpgsqlConnection connection)
    {
        // Open connection outside of 'using' block to keep it open for the entire method
        connection.Open();

        // Create the table if it doesn't exist
        var createTableCommand = @"
            CREATE TABLE IF NOT EXISTS Search (
                BookId INT NOT NULL,
                Title VARCHAR(255) NOT NULL,
                CategoryId INT NOT NULL,
                Category VARCHAR(50) NOT NULL,
                PublishedYear INT NOT NULL,
                Price DECIMAL(10, 2) NOT NULL
            )";

        using (var command = new NpgsqlCommand(createTableCommand, connection))
        {
            command.ExecuteNonQuery();  // Execute the command
            Console.WriteLine("Table 'Search' created (if not already exists).");
        }

        connection.Close();  // Make sure to close the connection after operations
    }


    private static void SeedDatabase(NpgsqlConnection connection)

    {
        // Open connection outside of 'using' block to keep it open for the entire method
        connection.Open();

        // First, check if the table is empty
        string checkEmptyTableQuery = "SELECT COUNT(*) FROM Search";

        using (var checkCommand = new NpgsqlCommand(checkEmptyTableQuery, connection))
        {
            var count = (long)checkCommand.ExecuteScalar();

            if (count == 0)  // Table is empty, so we insert data
            {
                var seedCommand = @"
                    INSERT INTO Search (BookId, Title, CategoryId, Category, PublishedYear, Price)
                    VALUES
                    (1, 'The Great Gatsby', 1, 'Fiction', 1925, 10.99),
                    (2, 'Gravity''s Rainbow', 3, 'Postmodern Fiction', 1973, 15.49),
                    (3, 'The Crying of Lot 49', 3, 'Postmodern Fiction', 1966, 8.99),
                    (4, 'V.', 3, 'Postmodern Fiction', 1963, 9.99),
                    (5, 'Inherent Vice', 3, 'Postmodern Fiction', 2009, 12.99),
                    (6, 'Mason & Dixon', 3, 'Postmodern Fiction', 1997, 14.99),
                    (7, 'Against the Day', 3, 'Postmodern Fiction', 2006, 16.49),
                    (8, 'Bleeding Edge', 3, 'Postmodern Fiction', 2013, 13.49),
                    (9, 'The Sot-Weed Factor', 4, 'Postmodern Fiction', 1960, 10.99),
                    (10, 'Slaughterhouse-Five', 2, 'Science Fiction', 1969, 9.49),
                    (11, 'The Man Who Fell to Earth', 4, 'Science Fiction', 1963, 11.99),
                    (12, 'The Master and Margarita', 4, 'Magical Realism', 1967, 12.49),
                    (13, 'Foucault''s Pendulum', 3, 'Postmodern Fiction', 1988, 15.99),
                    (14, 'The SearchResult of Daniel', 2, 'Historical Fiction', 1971, 10.99),
                    (15, 'The Secret History', 3, 'Mystery', 1992, 13.99),
                    (16, 'The Illuminatus! Trilogy', 3, 'Postmodern Fiction', 1975, 17.99),
                    (17, 'House of Leaves', 3, 'Postmodern Fiction', 2000, 14.49),
                    (18, 'White Teeth', 1, 'Fiction', 2000, 11.49),
                    (19, 'The Yiddish Policemen''s Union', 2, 'Crime Fiction', 2007, 10.99),
                    (20, 'The New York Trilogy', 3, 'Postmodern Fiction', 1987, 12.99)
                ";

                using (var insertCommand = new NpgsqlCommand(seedCommand, connection))
                {
                    insertCommand.ExecuteNonQuery();  // Insert the seed data
                    Console.WriteLine("Seed data inserted into 'Search' table.");
                }
            }
            else
            {
                Console.WriteLine("Table 'Search' is not empty, skipping seed data.");
            }
        }

        connection.Close();  // Ensure the connection is closed
    }
}


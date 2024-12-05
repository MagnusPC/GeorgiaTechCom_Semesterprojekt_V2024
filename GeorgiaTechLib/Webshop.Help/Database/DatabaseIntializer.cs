using Npgsql;

public class DatabaseService
{
    private readonly string _connectionString;

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

    }

    private static void CreateDatabase(NpgsqlConnection connection)
    {
        
        connection.Open();

        string createDBcommand = "CREATE DATABASE psuwebshop";

        using var command = new NpgsqlCommand(createDBcommand, connection);
        command.ExecuteNonQuery();
    }

    private static void CreateTable(NpgsqlConnection connection)
    {
        connection.Open();

        var crateTableCommand = @"
        CREATE TABLE IF NOT EXISTS Search (
            BookId INT NOT NULL,
            Title VARCHAR(255) NOT NULL,
            CategoryId INT NOT NULL,
            Category VARCHAR(50) NOT NULL,
            PublishedYear INT NOT NULL,
            Price DECIMAL(10, 2) NOT NULL
        )";

        using var command = new NpgsqlCommand(crateTableCommand, connection);
        command.ExecuteNonQuery();
    }

    private static void SeedDatabase(NpgsqlConnection connection)
    {
        var seedCommand = @"-- Insert books with price instead of author
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

        using var command = new NpgsqlCommand(seedCommand, connection);
        command.ExecuteNonQuery();
    }
}

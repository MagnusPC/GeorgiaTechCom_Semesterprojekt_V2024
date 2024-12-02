using Npgsql;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PostgresConnection");
    }

    public async Task InitializeDatabase()
    {
        using var connection = new NpgsqlConnection(_connectionString);

        await CreateTable(connection);
        await SeedDatabase(connection);

    }

    private async Task CreateTable(NpgsqlConnection connection)
    {
        await connection.OpenAsync();

        var crateTableCommand = @"
            CREATE TABLE IF NOT EXISTS Search (
                BookId INT NOT NULL,
                Title VARCHAR(255) NOT NULL,
                Author VARCHAR(255) NOT NULL,
                Categoryid INT NOT NULL,
                Categorty VARCHAR(50) NOT NULL,
                PublishedYear INT NOT NULL
            )";

        using var command = new NpgsqlCommand(crateTableCommand, connection);
        await command.ExecuteNonQueryAsync();
    }

    private async Task SeedDatabase(NpgsqlConnection connection)
    {
        var seedCommand = @"-- Insert books related to Pynchon and postmodern works
                INSERT INTO Search (BookId, Title, Author, Categoryid, Category, PublishedYear)
                VALUES
                (1, 'The Great Gatsby', 'F. Scott Fitzgerald', 1, 'Fiction', 1925),
                (2, 'Gravity''s Rainbow', 'Thomas Pynchon', 3, 'Postmodern Fiction', 1973),
                (3, 'The Crying of Lot 49', 'Thomas Pynchon', 3, 'Postmodern Fiction', 1966),
                (4, 'V.', 'Thomas Pynchon', 3, 'Postmodern Fiction', 1963),
                (5, 'Inherent Vice', 'Thomas Pynchon', 3, 'Postmodern Fiction', 2009),
                (6, 'Mason & Dixon', 'Thomas Pynchon', 3, 'Postmodern Fiction', 1997),
                (7, 'Against the Day', 'Thomas Pynchon', 3, 'Postmodern Fiction', 2006),
                (8, 'Bleeding Edge', 'Thomas Pynchon', 3, 'Postmodern Fiction', 2013),
                (9, 'The Sot-Weed Factor', 'John Barth', 4, 'Postmodern Fiction', 1960),
                (10, 'Slaughterhouse-Five', 'Kurt Vonnegut', 2, 'Science Fiction', 1969),
                (11, 'The Man Who Fell to Earth', 'Walter Tevis', 4, 'Science Fiction', 1963),
                (12, 'The Master and Margarita', 'Mikhail Bulgakov', 4, 'Magical Realism', 1967),
                (13, 'Foucault''s Pendulum', 'Umberto Eco', 3, 'Postmodern Fiction', 1988),
                (14, 'The SearchResult of Daniel', 'E.L. Doctorow', 2, 'Historical Fiction', 1971),
                (15, 'The Secret History', 'Donna Tartt', 3, 'Mystery', 1992),
                (16, 'The Illuminatus! Trilogy', 'Robert Shea & Robert Anton Wilson', 3, 'Postmodern Fiction', 1975),
                (17, 'House of Leaves', 'Mark Z. Danielewski', 3, 'Postmodern Fiction', 2000),
                (18, 'White Teeth', 'Zadie Smith', 1, 'Fiction', 2000),
                (19, 'The Yiddish Policemen''s Union', 'Michael Chabon', 2, 'Crime Fiction', 2007),
                (20, 'The New York Trilogy', 'Paul Auster', 3, 'Postmodern Fiction', 1987)
                ";

        using var command = new NpgsqlCommand(seedCommand, connection);
        await command.ExecuteNonQueryAsync();
    }
}

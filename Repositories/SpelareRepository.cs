using Del2databasboll.Models;
using Microsoft.Data.Sqlite;

namespace Del2databasboll.Repositories;

public class SpelareRepository : ISpelarRepository
{
    private readonly string _connectionString;

    public SpelareRepository(string dbFilNamn = "spelare.db")
    {
        _connectionString = $"Data Source={dbFilNamn}";
    }

    public void InitieraDatabas()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS spelare (
                id INTEGER PRIMARY KEY,
                namn TEXT NOT NULL,
                trojnummer INTEGER NOT NULL,
                mal INTEGER NOT NULL,
                matcher_spelade INTEGER NOT NULL
            );";

        command.ExecuteNonQuery();
    }

    public void LäggTillSpelare(Spelare spelare)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO spelare (id, namn, trojnummer, mal, matcher_spelade)
            VALUES ($id, $namn, $trojnummer, $mal, $matcher);";
        command.Parameters.AddWithValue("$id", spelare.Id);
        command.Parameters.AddWithValue("$namn", spelare.Namn);
        command.Parameters.AddWithValue("$trojnummer", spelare.Tröjnummer);
        command.Parameters.AddWithValue("$mal", spelare.Mål);
        command.Parameters.AddWithValue("$matcher", spelare.MatcherSpelade);

        command.ExecuteNonQuery();
    }

    public List<Spelare> HämtaAlla()
    {
        var lista = new List<Spelare>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT id, namn, trojnummer, mal, matcher_spelade FROM spelare ORDER BY id;";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Spelare
            {
                Id = reader.GetInt32(0),
                Namn = reader.GetString(1),
                Tröjnummer = reader.GetInt32(2),
                Mål = reader.GetInt32(3),
                MatcherSpelade = reader.GetInt32(4)
            });
        }

        return lista;
    }

    public Spelare? HämtaSpelareById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT id, namn, trojnummer, mal, matcher_spelade
            FROM spelare
            WHERE id = $id
            LIMIT 1;";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Spelare
        {
            Id = reader.GetInt32(0),
            Namn = reader.GetString(1),
            Tröjnummer = reader.GetInt32(2),
            Mål = reader.GetInt32(3),
            MatcherSpelade = reader.GetInt32(4)
        };
    }

    public Spelare? SökSpelare(string namn)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT id, namn, trojnummer, mal, matcher_spelade
            FROM spelare
            WHERE LOWER(namn) LIKE LOWER($namn)
            LIMIT 1;";
        command.Parameters.AddWithValue("$namn", $"%{namn}%");

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Spelare
        {
            Id = reader.GetInt32(0),
            Namn = reader.GetString(1),
            Tröjnummer = reader.GetInt32(2),
            Mål = reader.GetInt32(3),
            MatcherSpelade = reader.GetInt32(4)
        };
    }

    public bool TaBortSpelare(string namn)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM spelare WHERE LOWER(namn) = LOWER($namn);";
        command.Parameters.AddWithValue("$namn", namn);

        int rader = command.ExecuteNonQuery();
        return rader > 0;
    }

    public bool UppdateraSpelare(Spelare spelare)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE spelare
            SET namn = $namn,
                trojnummer = $trojnummer,
                mal = $mal,
                matcher_spelade = $matcher
            WHERE id = $id;";
        command.Parameters.AddWithValue("$id", spelare.Id);
        command.Parameters.AddWithValue("$namn", spelare.Namn);
        command.Parameters.AddWithValue("$trojnummer", spelare.Tröjnummer);
        command.Parameters.AddWithValue("$mal", spelare.Mål);
        command.Parameters.AddWithValue("$matcher", spelare.MatcherSpelade);

        int rader = command.ExecuteNonQuery();
        return rader > 0;
    }

    public bool FinnsSpelare(string namn)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM spelare WHERE LOWER(namn) = LOWER($namn);";
        command.Parameters.AddWithValue("$namn", namn);

        long antal = (long)(command.ExecuteScalar() ?? 0L);
        return antal > 0;
    }
}

using System.Collections.Generic;
using System.Data.SqlClient;
using NexusLauncher.Models;

namespace NexusLauncher.DAL
{
    public class GameDAL
    {
        public List<Game> GetAllGames()
        {
            var games = new List<Game>();

            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("SELECT Id, Title, InstallPath, IsInstalled FROM Games", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        games.Add(new Game
                        {
                            Id = (int)r["Id"],
                            Title = (string)r["Title"],
                            InstallPath = r["InstallPath"] as string,
                            IsInstalled = (bool)r["IsInstalled"]
                        });
                    }
                }
            }

            return games;
        }

        public Game GetById(int id)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("SELECT Id, Title, InstallPath, IsInstalled FROM Games WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new Game
                        {
                            Id = (int)r["Id"],
                            Title = (string)r["Title"],
                            InstallPath = r["InstallPath"] as string,
                            IsInstalled = (bool)r["IsInstalled"]
                        };
                    }
                }
            }
            return null;
        }

        public int Create(Game game)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(
                "INSERT INTO Games (Title, InstallPath, IsInstalled) VALUES (@t, @p, @i); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                conn))
            {
                cmd.Parameters.AddWithValue("@t", game.Title ?? (object)System.DBNull.Value);
                cmd.Parameters.AddWithValue("@p", game.InstallPath ?? (object)System.DBNull.Value);
                cmd.Parameters.AddWithValue("@i", game.IsInstalled);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return (result == null || result == System.DBNull.Value) ? 0 : (int)result;
            }
        }

        public bool Update(Game game)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(
                "UPDATE Games SET Title = @t, InstallPath = @p, IsInstalled = @i WHERE Id = @id",
                conn))
            {
                cmd.Parameters.AddWithValue("@t", game.Title ?? (object)System.DBNull.Value);
                cmd.Parameters.AddWithValue("@p", game.InstallPath ?? (object)System.DBNull.Value);
                cmd.Parameters.AddWithValue("@i", game.IsInstalled);
                cmd.Parameters.AddWithValue("@id", game.Id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("DELETE FROM Games WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
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
            using (var cmd = new SqlCommand(
                "SELECT Id, Title, InstallPath, IsInstalled, Price, Image, ImageFileName, ImageContentType FROM Games",
                conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        games.Add(MapReaderToGame(r));
                    }
                }
            }

            return games;
        }

        public Game GetById(int id)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(
                "SELECT Id, Title, InstallPath, IsInstalled, Price, Image, ImageFileName, ImageContentType FROM Games WHERE Id = @id",
                conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return MapReaderToGame(r);
                    }
                }
            }
            return null;
        }

        public int Create(Game game)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                INSERT INTO Games (Title, InstallPath, IsInstalled, Price, Image, ImageFileName, ImageContentType)
                VALUES (@t, @p, @i, @price, @img, @imgName, @imgType);
                SELECT CAST(SCOPE_IDENTITY() AS INT);", conn))
            {
                cmd.Parameters.AddWithValue("@t", game.Title ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@p", game.InstallPath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@i", game.IsInstalled);
                cmd.Parameters.AddWithValue("@price", game.Price);
                cmd.Parameters.AddWithValue("@img", game.Image ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@imgName", game.ImageFileName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@imgType", game.ImageContentType ?? (object)DBNull.Value);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0 : (int)result;
            }
        }

        public bool Update(Game game)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE Games
                SET Title = @t,
                    InstallPath = @p,
                    IsInstalled = @i,
                    Price = @price,
                    Image = @img,
                    ImageFileName = @imgName,
                    ImageContentType = @imgType
                WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@t", game.Title ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@p", game.InstallPath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@i", game.IsInstalled);
                cmd.Parameters.AddWithValue("@price", game.Price);
                cmd.Parameters.AddWithValue("@img", game.Image ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@imgName", game.ImageFileName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@imgType", game.ImageContentType ?? (object)DBNull.Value);
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

        // --------------------------
        // Métodos de biblioteca por usuario
        // --------------------------

        public List<Game> GetLibraryByUserId(int userId)
        {
            var list = new List<Game>();

            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                SELECT g.Id, g.Title, g.InstallPath, g.IsInstalled, g.Price,
                       g.Image, g.ImageFileName, g.ImageContentType,
                       ug.PurchaseDate, ug.Owned
                FROM Games g
                INNER JOIN UserGames ug ON ug.GameId = g.Id
                WHERE ug.UserId = @uid
                ORDER BY ug.PurchaseDate DESC", conn))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                conn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var g = MapReaderToGame(r);
                        g.PurchaseDate = r["PurchaseDate"] == DBNull.Value ? (DateTime?)null : (DateTime)r["PurchaseDate"];
                        g.Owned = r["Owned"] == DBNull.Value ? true : Convert.ToBoolean(r["Owned"]);
                        list.Add(g);
                    }
                }
            }

            return list;
        }

        public bool AddGameToUser(int userId, int gameId)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM UserGames WHERE UserId = @uid AND GameId = @gid)
                    INSERT INTO UserGames (UserId, GameId, PurchaseDate, Owned) VALUES (@uid, @gid, GETDATE(), 1);
                ", conn))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@gid", gameId);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool RemoveGameFromUser(int userId, int gameId)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("DELETE FROM UserGames WHERE UserId = @uid AND GameId = @gid", conn))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@gid", gameId);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool UserOwnsGame(int userId, int gameId)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("SELECT 1 FROM UserGames WHERE UserId = @uid AND GameId = @gid", conn))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@gid", gameId);
                conn.Open();
                var exists = cmd.ExecuteScalar();
                return exists != null;
            }
        }

        // Helper: mapea un DataReader a un objeto Game
        private Game MapReaderToGame(SqlDataReader r)
        {
            return new Game
            {
                Id = r["Id"] == DBNull.Value ? 0 : (int)r["Id"],
                Title = r["Title"] as string,
                InstallPath = r["InstallPath"] as string,
                IsInstalled = r["IsInstalled"] == DBNull.Value ? false : Convert.ToBoolean(r["IsInstalled"]),
                Price = r["Price"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Price"]),
                Image = r["Image"] == DBNull.Value ? null : (byte[])r["Image"],
                ImageFileName = r["ImageFileName"] as string,
                ImageContentType = r["ImageContentType"] as string
            };
        }
    }
}
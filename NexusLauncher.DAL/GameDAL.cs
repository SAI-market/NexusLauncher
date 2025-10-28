using NexusLauncher.Models;
using System; // Necesario para DBNull
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NexusLauncher.DAL
{
    public class GameDAL
    {
        private string connectionString = DbConfig.ConnectionString;

        // --- MÉTODOS DE TIENDA Y BIBLIOTECA (Sprint 3/4) ---

        public List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Usamos las columnas de tu modelo: Title, InstallPath, Price
                string query = "SELECT ID, Title, InstallPath, Price FROM Games";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    games.Add(new Game
                    {
                        Id = (int)reader["ID"],
                        Title = (string)reader["Title"],
                        InstallPath = (string)reader["InstallPath"],
                        Price = reader["Price"] != DBNull.Value ? (decimal)reader["Price"] : 0m
                    });
                }
            }
            return games;
        }

        public List<Game> GetGamesByUserId(int userId)
        {
            List<Game> games = new List<Game>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Usamos las columnas de tu modelo: Title, InstallPath
                string query = @"
                    SELECT j.ID, j.Title, j.InstallPath 
                    FROM Games j
                    INNER JOIN UserGames uj ON j.ID = uj.GameID
                    WHERE uj.UserID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    games.Add(new Game
                    {
                        Id = (int)reader["ID"],
                        Title = (string)reader["Title"],
                        InstallPath = (string)reader["InstallPath"]
                    });
                }
            }
            return games;
        }

        public bool BuyGame(int userId, int gameId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(1) FROM UserGames WHERE UserID = @UserID AND GameID = @GameID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@UserID", userId);
                checkCmd.Parameters.AddWithValue("@GameID", gameId);

                connection.Open();
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return false; // Ya lo tiene
                }

                string insertQuery = "INSERT INTO UserGames (UserID, GameID) VALUES (@UserID, @GameID)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@UserID", userId);
                insertCmd.Parameters.AddWithValue("@GameID", gameId);

                int result = insertCmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        // --- MÉTODOS CRUD DE ADMIN (Sprint 2 - FALTANTES) ---

        public Game GetGameById(int gameId)
        {
            Game game = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Usamos las columnas de tu modelo: Title, InstallPath, Price, Image
                string query = "SELECT ID, Title, InstallPath, Price, Image FROM Games WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", gameId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    game = new Game
                    {
                        Id = (int)reader["ID"],
                        Title = (string)reader["Title"],
                        InstallPath = (string)reader["InstallPath"],
                        Price = reader["Price"] != DBNull.Value ? (decimal)reader["Price"] : 0m,
                        Image = reader["Image"] != DBNull.Value ? (byte[])reader["Image"] : null
                    };
                }
            }
            return game;
        }

        public void CreateGame(Game game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Games (Title, InstallPath, Price, Image) VALUES (@Title, @InstallPath, @Price, @Image)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", game.Title);
                command.Parameters.AddWithValue("@InstallPath", game.InstallPath);
                command.Parameters.AddWithValue("@Price", game.Price);
                command.Parameters.AddWithValue("@Image", (object)game.Image ?? DBNull.Value); // Manejo de imagen nula
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateGame(Game game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Games SET Title = @Title, InstallPath = @InstallPath, Price = @Price, Image = @Image WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", game.Title);
                command.Parameters.AddWithValue("@InstallPath", game.InstallPath);
                command.Parameters.AddWithValue("@Price", game.Price);
                command.Parameters.AddWithValue("@Image", (object)game.Image ?? DBNull.Value); // Manejo de imagen nula
                command.Parameters.AddWithValue("@ID", game.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
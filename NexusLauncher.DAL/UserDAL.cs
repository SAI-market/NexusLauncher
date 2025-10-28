using NexusLauncher.Models;
using System.Data.SqlClient;

namespace NexusLauncher.DAL
{
    public class UserDAL
    {
        private string connectionString = DbConfig.ConnectionString;

        public User Login(string username, string password)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Asumiendo que tus columnas son 'NombreUsuario', 'Password', 'Email', 'IsAdmin'
                string query = "SELECT ID, NombreUsuario, Email, IsAdmin FROM Users WHERE NombreUsuario = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        Id = (int)reader["ID"],
                        NombreUsuario = (string)reader["NombreUsuario"],
                        Email = (string)reader["Email"],
                        IsAdmin = (bool)reader["IsAdmin"]
                    };
                }
            }
            return user;
        }

        public bool Register(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (NombreUsuario, Email, Password, IsAdmin) VALUES (@Username, @Email, @Password, 0)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.NombreUsuario);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Password = @Password WHERE ID = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
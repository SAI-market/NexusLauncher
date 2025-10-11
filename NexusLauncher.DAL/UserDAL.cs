using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NexusLauncher.Models;

namespace NexusLauncher.DAL
{
    public class UserDAL
    {
        public User GetByUsername(string username)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("SELECT Id, Username, Password, DisplayName, Email FROM Users WHERE Username = @u", conn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new User
                        {
                            Id = (int)r["Id"],
                            Username = (string)r["Username"],
                            Password = (string)r["Password"],
                            DisplayName = r["DisplayName"] as string,
                            Email = r["Email"] as string
                        };
                    }
                }
            }
            return null;
        }

        public User GetByEmail(string email)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("SELECT Id, Username, Password, DisplayName, Email FROM Users WHERE Email = @e", conn))
            {
                cmd.Parameters.AddWithValue("@e", email);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new User
                        {
                            Id = (int)r["Id"],
                            Username = (string)r["Username"],
                            Password = (string)r["Password"],
                            DisplayName = r["DisplayName"] as string,
                            Email = r["Email"] as string
                        };
                    }
                }
            }
            return null;
        }

        public int CreateUser(User user)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(
                "INSERT INTO Users (Username, Password, DisplayName, Email) VALUES (@u, @p, @d, @e); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                conn))
            {
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@p", user.Password);
                cmd.Parameters.AddWithValue("@d", user.DisplayName ?? (object)System.DBNull.Value);
                cmd.Parameters.AddWithValue("@e", user.Email ?? (object)System.DBNull.Value);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return (result == null || result == System.DBNull.Value) ? 0 : (int)result;
            }
        }
    }
}
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
            using (var cmd = new SqlCommand("SELECT Id, Username, Password, DisplayName FROM Users WHERE Username = @u", conn))
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
                            DisplayName = r["DisplayName"] as string
                        };
                    }
                }
            }
            return null;
        }
    }
}

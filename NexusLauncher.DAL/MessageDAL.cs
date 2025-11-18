using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NexusLauncher.Models;

namespace NexusLauncher.DAL
{
    public class MessageDAL
    {
        private readonly string _conn = DbConfig.ConnectionString;

        public int CreateMessage(Message m)
        {
            if (m == null) throw new ArgumentNullException(nameof(m));

            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO Messages (FromUserId, ToUserId, Body, SentAt, IsRead)
                    VALUES (@from, @to, @body, @sent, @isRead);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";
                cmd.Parameters.AddWithValue("@from", m.FromUserId);
                cmd.Parameters.AddWithValue("@to", m.ToUserId);
                cmd.Parameters.AddWithValue("@body", m.Body ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@sent", m.SentAt == default(DateTime) ? DateTime.UtcNow : m.SentAt);
                cmd.Parameters.AddWithValue("@isRead", m.IsRead);

                cn.Open();
                var res = cmd.ExecuteScalar();
                return (res == null || res == DBNull.Value) ? 0 : (int)res;
            }
        }

        public List<Message> GetConversation(int userAId, int userBId, int limit = 100)
        {
            var list = new List<Message>();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT TOP(@limit) Id, FromUserId, ToUserId, Body, SentAt, IsRead
                    FROM Messages
                    WHERE (FromUserId = @a AND ToUserId = @b) OR (FromUserId = @b AND ToUserId = @a)
                    ORDER BY SentAt DESC
                ";
                cmd.Parameters.AddWithValue("@limit", limit);
                cmd.Parameters.AddWithValue("@a", userAId);
                cmd.Parameters.AddWithValue("@b", userBId);

                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Message
                        {
                            Id = rd.GetInt32(0),
                            FromUserId = rd.GetInt32(1),
                            ToUserId = rd.GetInt32(2),
                            Body = rd.IsDBNull(3) ? null : rd.GetString(3),
                            SentAt = rd.IsDBNull(4) ? DateTime.MinValue : rd.GetDateTime(4),
                            IsRead = rd.IsDBNull(5) ? false : rd.GetBoolean(5)
                        });
                    }
                }
            }
            return list;
        }

        public bool MarkAsRead(int messageId)
        {
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = "UPDATE Messages SET IsRead = 1 WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", messageId);
                cn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public List<Message> GetUnreadMessagesForUser(int userId)
        {
            var list = new List<Message>();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT Id, FromUserId, ToUserId, Body, SentAt, IsRead
                    FROM Messages
                    WHERE ToUserId = @to AND IsRead = 0
                    ORDER BY SentAt ASC
                ";
                cmd.Parameters.AddWithValue("@to", userId);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Message
                        {
                            Id = rd.GetInt32(0),
                            FromUserId = rd.GetInt32(1),
                            ToUserId = rd.GetInt32(2),
                            Body = rd.IsDBNull(3) ? null : rd.GetString(3),
                            SentAt = rd.IsDBNull(4) ? DateTime.MinValue : rd.GetDateTime(4),
                            IsRead = rd.IsDBNull(5) ? false : rd.GetBoolean(5)
                        });
                    }
                }
            }
            return list;
        }
    }
}

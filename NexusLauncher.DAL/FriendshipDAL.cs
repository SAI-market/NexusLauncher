using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NexusLauncher.Models;

namespace NexusLauncher.DAL
{
    public class FriendshipDAL
    {
        private readonly string _conn = DbConfig.ConnectionString;

        /// <summary>
        /// Devuelve la lista de amigos (accepted) del usuario.
        /// </summary>
        public List<FriendViewModel> GetFriendsForUser(int userId)
        {
            var list = new List<FriendViewModel>();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT u.Id, u.Username, u.DisplayName
                    FROM Users u
                    INNER JOIN Friendships f 
                        ON ((f.UserId1 = @userId AND f.UserId2 = u.Id) OR (f.UserId2 = @userId AND f.UserId1 = u.Id))
                    WHERE f.Status = 'Accepted'
                    ORDER BY COALESCE(u.DisplayName, u.Username)
                ";
                cmd.Parameters.AddWithValue("@userId", userId);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new FriendViewModel
                        {
                            Id = rd.GetInt32(0),
                            Username = rd.IsDBNull(1) ? null : rd.GetString(1),
                            DisplayName = rd.IsDBNull(2) ? null : rd.GetString(2),
                            FriendStatus = "Friend",
                            Presence = "Offline"
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Devuelve todos los usuarios con el estado de relación relativo al usuario actual.
        /// </summary>
        public List<FriendViewModel> GetAllUsersWithFriendStatus(int currentUserId)
        {
            var list = new List<FriendViewModel>();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT u.Id, u.Username, u.DisplayName,
                           f.Id as FriendshipId, f.Status, f.RequestedBy
                    FROM Users u
                    LEFT JOIN Friendships f
                        ON ( (f.UserId1 = @current AND f.UserId2 = u.Id) OR (f.UserId2 = @current AND f.UserId1 = u.Id) )
                    WHERE u.Id <> @current
                    ORDER BY COALESCE(u.DisplayName, u.Username)
                ";
                cmd.Parameters.AddWithValue("@current", currentUserId);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var uid = rd.GetInt32(0);
                        var username = rd.IsDBNull(1) ? null : rd.GetString(1);
                        var display = rd.IsDBNull(2) ? null : rd.GetString(2);

                        var friendStatus = "NotFriend";
                        if (!rd.IsDBNull(3))
                        {
                            var status = rd.IsDBNull(4) ? null : rd.GetString(4);
                            var requestedBy = rd.IsDBNull(5) ? 0 : rd.GetInt32(5);
                            if (status == "Accepted") friendStatus = "Friend";
                            else if (status == "Pending")
                            {
                                if (requestedBy == currentUserId) friendStatus = "PendingSent";
                                else friendStatus = "PendingReceived";
                            }
                        }

                        list.Add(new FriendViewModel
                        {
                            Id = uid,
                            Username = username,
                            DisplayName = display,
                            FriendStatus = friendStatus,
                            Presence = "Offline"
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Crea una solicitud de amistad (Pending). Devuelve true si se insertó.
        /// </summary>
        public bool CreateFriendRequest(int fromUserId, int toUserId)
        {
            // Evitar solicitudes a sí mismo
            if (fromUserId == toUserId) return false;

            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cn.Open();
                // Verificamos si ya existe friendship entre ambos
                cmd.CommandText = @"
                    SELECT TOP 1 Id, Status FROM Friendships
                    WHERE (UserId1 = @a AND UserId2 = @b) OR (UserId1 = @b AND UserId2 = @a)
                ";
                cmd.Parameters.AddWithValue("@a", fromUserId);
                cmd.Parameters.AddWithValue("@b", toUserId);
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        var status = rd.IsDBNull(1) ? null : rd.GetString(1);
                        if (status == "Accepted" || status == "Pending") return false;
                    }
                }

                // Insertamos la solicitud
                using (var ins = cn.CreateCommand())
                {
                    ins.CommandText = @"
                        INSERT INTO Friendships (UserId1, UserId2, Status, RequestedBy, CreatedAt)
                        VALUES (@u1, @u2, 'Pending', @req, GETDATE())
                    ";
                    ins.Parameters.AddWithValue("@u1", fromUserId);
                    ins.Parameters.AddWithValue("@u2", toUserId);
                    ins.Parameters.AddWithValue("@req", fromUserId);
                    var rows = ins.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        /// <summary>
        /// Devuelve las solicitudes PENDIENTES que el usuario recibió (es decir: pending donde RequestedBy != currentUser).
        /// </summary>
        public List<FriendRequestDto> GetReceivedRequests(int currentUserId)
        {
            var list = new List<FriendRequestDto>();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT f.Id, f.RequestedBy, u.DisplayName, f.CreatedAt
                    FROM Friendships f
                    INNER JOIN Users u ON u.Id = f.RequestedBy
                    WHERE f.Status = 'Pending' 
                      AND (f.UserId1 = @current OR f.UserId2 = @current)
                      AND f.RequestedBy <> @current
                    ORDER BY f.CreatedAt DESC
                ";
                cmd.Parameters.AddWithValue("@current", currentUserId);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new FriendRequestDto
                        {
                            FriendshipId = rd.GetInt32(0),
                            FromUserId = rd.GetInt32(1),
                            FromDisplayName = rd.IsDBNull(2) ? null : rd.GetString(2),
                            CreatedAt = rd.GetDateTime(3)
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Cambia el estado a Accepted para la friendship indicada (si estaba Pending).
        /// </summary>
        public bool AcceptRequest(int friendshipId)
        {
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    UPDATE Friendships
                    SET Status = 'Accepted'
                    WHERE Id = @id AND Status = 'Pending'
                ";
                cmd.Parameters.AddWithValue("@id", friendshipId);
                cn.Open();
                var affected = cmd.ExecuteNonQuery();
                return affected > 0;
            }
        }

        /// <summary>
        /// Rechaza o borra la solicitud (Pending).
        /// </summary>
        public bool RejectRequest(int friendshipId)
        {
            using (var cn = new SqlConnection(_conn))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM Friendships WHERE Id = @id AND Status = 'Pending'";
                cmd.Parameters.AddWithValue("@id", friendshipId);
                cn.Open();
                var affected = cmd.ExecuteNonQuery();
                return affected > 0;
            }
        }
    }
}

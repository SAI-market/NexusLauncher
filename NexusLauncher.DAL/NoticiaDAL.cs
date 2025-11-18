using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NexusLauncher.Models;

namespace NexusLauncher.DAL
{
    public class NoticiaDAL
    {
        public List<Noticia> GetAll()
        {
            var list = new List<Noticia>();

            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(
                "SELECT Id, Titulo, Contenido, FechaPublicacion FROM Noticias ORDER BY FechaPublicacion DESC",
                conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(MapReaderToNoticia(r));
                    }
                }
            }

            return list;
        }

        public Noticia GetById(int id)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(
                "SELECT Id, Titulo, Contenido, FechaPublicacion FROM Noticias WHERE Id = @id",
                conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                        return MapReaderToNoticia(r);
                }
            }

            return null;
        }

        public int Create(Noticia noticia)
        {
            if (noticia == null) throw new ArgumentNullException(nameof(noticia));

            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                INSERT INTO Noticias (Titulo, Contenido, FechaPublicacion)
                VALUES (@titulo, @contenido, @fecha);
                SELECT CAST(SCOPE_IDENTITY() AS INT);", conn))
            {
                cmd.Parameters.AddWithValue("@titulo", noticia.Titulo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@contenido", noticia.Contenido ?? (object)DBNull.Value);
                // Si querés que la BD ponga la fecha por defecto, podés enviar DBNull.Value aquí cuando FechaPublicacion = default
                if (noticia.FechaPublicacion == default(DateTime))
                    cmd.Parameters.AddWithValue("@fecha", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@fecha", noticia.FechaPublicacion);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0 : (int)result;
            }
        }

        public bool Update(Noticia noticia)
        {
            if (noticia == null) throw new ArgumentNullException(nameof(noticia));

            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE Noticias
                SET Titulo = @titulo,
                    Contenido = @contenido,
                    FechaPublicacion = @fecha
                WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@titulo", noticia.Titulo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@contenido", noticia.Contenido ?? (object)DBNull.Value);
                if (noticia.FechaPublicacion == default(DateTime))
                    cmd.Parameters.AddWithValue("@fecha", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@fecha", noticia.FechaPublicacion);
                cmd.Parameters.AddWithValue("@id", noticia.Id);

                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand("DELETE FROM Noticias WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        /// <summary>
        /// Devuelve las N noticias más recientes (por defecto 10).
        /// </summary>
        public List<Noticia> GetLatest(int count = 10)
        {
            var list = new List<Noticia>();

            using (var conn = new SqlConnection(DbConfig.ConnectionString))
            using (var cmd = new SqlCommand(@"
                SELECT TOP(@count) Id, Titulo, Contenido, FechaPublicacion
                FROM Noticias
                ORDER BY FechaPublicacion DESC", conn))
            {
                cmd.Parameters.AddWithValue("@count", count);
                conn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(MapReaderToNoticia(r));
                    }
                }
            }

            return list;
        }

        // Helper: mapea un SqlDataReader a Noticia
        private Noticia MapReaderToNoticia(SqlDataReader r)
        {
            return new Noticia
            {
                Id = r["Id"] == DBNull.Value ? 0 : (int)r["Id"],
                Titulo = r["Titulo"] as string,
                Contenido = r["Contenido"] as string,
                FechaPublicacion = r["FechaPublicacion"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["FechaPublicacion"]
            };
        }
    }
}

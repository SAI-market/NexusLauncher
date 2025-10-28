using NexusLauncher.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NexusLauncher.DAL
{
    public class NewsDAL
    {
        private string connectionString = DbConfig.ConnectionString;

        public List<News> GetLatestNews(int count)
        {
            List<News> newsList = new List<News>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP (@Count) ID, Title, Content, PublicationDate FROM News ORDER BY PublicationDate DESC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Count", count);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newsList.Add(new News
                    {
                        ID = (int)reader["ID"],
                        Title = (string)reader["Title"],
                        Content = (string)reader["Content"],
                        PublicationDate = (DateTime)reader["PublicationDate"]
                    });
                }
            }
            return newsList;
        }

        public News GetNewsById(int id)
        {
            News news = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Title, Content, PublicationDate FROM News WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    news = new News
                    {
                        ID = (int)reader["ID"],
                        Title = (string)reader["Title"],
                        Content = (string)reader["Content"],
                        PublicationDate = (DateTime)reader["PublicationDate"]
                    };
                }
            }
            return news;
        }

        public List<News> GetAllNews()
        {
            List<News> newsList = new List<News>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Title, PublicationDate FROM News ORDER BY PublicationDate DESC";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newsList.Add(new News
                    {
                        ID = (int)reader["ID"],
                        Title = (string)reader["Title"],
                        PublicationDate = (DateTime)reader["PublicationDate"]
                    });
                }
            }
            return newsList;
        }

        public void InsertNews(News news)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO News (Title, Content, PublicationDate) VALUES (@Title, @Content, @PublicationDate)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", news.Title);
                command.Parameters.AddWithValue("@Content", news.Content);
                command.Parameters.AddWithValue("@PublicationDate", DateTime.Now);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateNews(News news)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE News SET Title = @Title, Content = @Content WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", news.Title);
                command.Parameters.AddWithValue("@Content", news.Content);
                command.Parameters.AddWithValue("@ID", news.ID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteNews(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM News WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
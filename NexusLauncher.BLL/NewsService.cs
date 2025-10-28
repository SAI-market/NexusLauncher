using NexusLauncher.DAL;
using NexusLauncher.Models;
using System.Collections.Generic;

namespace NexusLauncher.BLL
{
    public class NewsService
    {
        
        private NewsDAL newsDAL = new NewsDAL();

        public List<News> GetLatestNews(int count)
        {
            return newsDAL.GetLatestNews(count);
        }

        public News GetNewsById(int id)
        {
            return newsDAL.GetNewsById(id);
        }

        public List<News> GetAllNews()
        {
            return newsDAL.GetAllNews();
        }

        public void SaveNews(News news)
        {
            if (news.ID > 0)
            {
                newsDAL.UpdateNews(news);
            }
            else
            {
                newsDAL.InsertNews(news);
            }
        }

        public void DeleteNews(int id)
        {
            newsDAL.DeleteNews(id);
        }
    }
}
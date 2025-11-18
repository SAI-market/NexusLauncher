using System;
using System.Collections.Generic;
using NexusLauncher.DAL;
using NexusLauncher.Models;

namespace NexusLauncher.BLL
{
    public class NoticiaService
    {
        private readonly NoticiaDAL _dal = new NoticiaDAL();

        public List<Noticia> GetAll()
        {
            return _dal.GetAll();
        }

        public List<Noticia> GetLatest(int count = 10)
        {
            return _dal.GetLatest(count);
        }

        public Noticia GetById(int id)
        {
            if (id <= 0) return null;
            return _dal.GetById(id);
        }

        public int Create(Noticia noticia)
        {
            if (noticia == null) throw new ArgumentNullException(nameof(noticia));
            if (string.IsNullOrWhiteSpace(noticia.Titulo)) throw new ArgumentException("El título no puede estar vacío.", nameof(noticia.Titulo));
            return _dal.Create(noticia);
        }

        public bool Update(Noticia noticia)
        {
            if (noticia == null) throw new ArgumentNullException(nameof(noticia));
            if (noticia.Id <= 0) throw new ArgumentException("Id inválido.", nameof(noticia.Id));
            return _dal.Update(noticia);
        }

        public bool Delete(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.", nameof(id));
            return _dal.Delete(id);
        }
    }
}

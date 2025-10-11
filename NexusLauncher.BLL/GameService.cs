using System;
using System.Collections.Generic;
using NexusLauncher.DAL;
using NexusLauncher.Models;

namespace NexusLauncher.BLL
{
    public class GameService
    {
        private readonly GameDAL _gameDAL = new GameDAL();

        public List<Game> GetLibrary()
        {
            return _gameDAL.GetAllGames();
        }

        public Game GetGame(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.", nameof(id));
            return _gameDAL.GetById(id);
        }

        // Crea un juego; devuelve el id generado (0 si falló).
        public int CreateGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (string.IsNullOrWhiteSpace(game.Title))
                throw new ArgumentException("El título no puede estar vacío.", nameof(game.Title));

            var newId = _gameDAL.Create(game);
            if (newId > 0) game.Id = newId;
            return newId;
        }

        // Actualiza un juego ya existente; devuelve true si se actualizó.
        public bool UpdateGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (game.Id <= 0) throw new ArgumentException("Id inválido.", nameof(game.Id));
            if (string.IsNullOrWhiteSpace(game.Title))
                throw new ArgumentException("El título no puede estar vacío.", nameof(game.Title));

            return _gameDAL.Update(game);
        }

        // Borra un juego por id; devuelve true si se borró.
        public bool DeleteGame(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.", nameof(id));
            return _gameDAL.Delete(id);
        }
    }
}

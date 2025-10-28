using System;
using System.Collections.Generic;
using NexusLauncher.DAL;
using NexusLauncher.Models;

namespace NexusLauncher.BLL
{
    public class GameService
    {
        private readonly GameDAL _gameDAL = new GameDAL();

        // tu GetLibrary() sin parámetros existente puede seguir devolviendo todos los juegos
        public List<Game> GetLibrary()
        {
            return _gameDAL.GetAllGames();
        }

        // obtener la biblioteca del usuario
        public List<Game> GetLibrary(User user)
        {
            if (user == null) return new List<Game>();
            return _gameDAL.GetLibraryByUserId(user.Id);
        }

        // GetGame, CreateGame, UpdateGame, DeleteGame
        public Game GetGame(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.", nameof(id));
            return _gameDAL.GetById(id);
        }

        public int CreateGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (string.IsNullOrWhiteSpace(game.Title))
                throw new ArgumentException("El título no puede estar vacío.", nameof(game.Title));

            var newId = _gameDAL.Create(game);
            if (newId > 0) game.Id = newId;
            return newId;
        }

        public bool UpdateGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (game.Id <= 0) throw new ArgumentException("Id inválido.", nameof(game.Id));
            if (string.IsNullOrWhiteSpace(game.Title))
                throw new ArgumentException("El título no puede estar vacío.", nameof(game.Title));

            return _gameDAL.Update(game);
        }

        public bool DeleteGame(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.", nameof(id));
            return _gameDAL.Delete(id);
        }

        // Wrappers de UserGames
        public bool AddGameToUser(User user, int gameId) => user != null && _gameDAL.AddGameToUser(user.Id, gameId);
        public bool RemoveGameFromUser(User user, int gameId) => user != null && _gameDAL.RemoveGameFromUser(user.Id, gameId);
        public bool UserOwnsGame(User user, int gameId) => user != null && _gameDAL.UserOwnsGame(user.Id, gameId);
    }
}

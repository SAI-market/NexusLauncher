using NexusLauncher.DAL;
using NexusLauncher.Models;
using System.Collections.Generic;

namespace NexusLauncher.BLL
{
    public class GameService
    {
        private GameDAL gameDAL = new GameDAL();

        // --- MÉTODOS DE TIENDA Y BIBLIOTECA (Sprint 3/4) ---

        public List<Game> GetAllGames()
        {
            return gameDAL.GetAllGames();
        }

        public List<Game> GetGamesByUserId(int userId)
        {
            return gameDAL.GetGamesByUserId(userId);
        }

        public bool BuyGame(int userId, int gameId)
        {
            if (userId <= 0 || gameId <= 0)
            {
                return false;
            }
            return gameDAL.BuyGame(userId, gameId);
        }

        // --- MÉTODOS CRUD DE ADMIN (Sprint 2 - FALTANTES) ---

        public Game GetGame(int gameId)
        {
            return gameDAL.GetGameById(gameId);
        }

        public void CreateGame(Game game)
        {
            // Aquí se pueden añadir validaciones, ej:
            // if (string.IsNullOrEmpty(game.Title))
            //     throw new Exception("El título es obligatorio");

            gameDAL.CreateGame(game);
        }

        public void UpdateGame(Game game)
        {
            // Aquí se pueden añadir validaciones
            gameDAL.UpdateGame(game);
        }
    }
}
using NexusLauncher.DAL;
using NexusLauncher.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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

        public bool InstallAndRegisterGameForUser(User user, Game game, string baseInstallRoot, out string error)
        {
            error = null;

            if (user == null) { error = "Usuario inválido."; return false; }
            if (game == null) { error = "Juego inválido."; return false; }
            if (string.IsNullOrWhiteSpace(baseInstallRoot)) { error = "Ruta de instalación inválida."; return false; }

            try
            {
                // Normalizar nombre de carpeta seguro (evitar caracteres inválidos)
                var invalidChars = Path.GetInvalidFileNameChars();
                var safeName = new string(game.Title?.Where(c => !invalidChars.Contains(c)).ToArray());
                if (string.IsNullOrWhiteSpace(safeName))
                    safeName = $"game_{game.Id}";

                // Crear ruta completa: baseInstallRoot\<safeName>
                string gameFolder = Path.Combine(baseInstallRoot, safeName);

                // Asegurarnos de que la carpeta exista
                Directory.CreateDirectory(gameFolder);

                // Crear archivo de prueba .txt
                string txtFileName = safeName + ".txt";
                string txtPath = Path.Combine(gameFolder, txtFileName);
                File.WriteAllText(txtPath, $"Archivo de prueba para \"{game.Title}\"{Environment.NewLine}Instalado: {DateTime.Now}", System.Text.Encoding.UTF8);

                // Crear run.bat que abre el txt con notepad (ejecutable de prueba)
                string batPath = Path.Combine(gameFolder, "run.bat");
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("@echo off");
                // Usamos start para abrir notepad sin bloquear el proceso que llama
                sb.AppendLine($"start \"\" \"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe")}\" \"{txtPath}\"");
                sb.AppendLine("exit");
                File.WriteAllText(batPath, sb.ToString(), System.Text.Encoding.ASCII);

                // Actualizar objeto en memoria para que el UI lo vea inmediatamente
                game.InstallPath = gameFolder;
                game.IsInstalled = true;

                // Guardar InstallPath en UserGames (instalación por usuario)
                var addOk = _gameDAL.AddGameToUser(user.Id, game.Id, gameFolder);
                if (!addOk)
                {
                    // intentamos limpiar los archivos creados para no dejar basura
                    try
                    {
                        if (Directory.Exists(gameFolder))
                            Directory.Delete(gameFolder, true);
                    }
                    catch
                    {
                        // ignorar errores de limpieza
                    }

                    error = "No se pudo registrar la instalación en la base de datos (UserGames).";
                    return false;
                }

                // Marcar flag global en Games (opcional)
                try
                {
                    _gameDAL.SetGameInstalledFlag(game.Id, true);
                }
                catch
                {
                    // No fatal: si falla el flag, no queremos revertir la instalación por el usuario
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }




        public Process LaunchGameProcess(Game game, out string error)
        {
            error = null;
            if (game == null) { error = "Juego inválido"; return null; }
            if (string.IsNullOrWhiteSpace(game.InstallPath)) { error = "InstallPath no definido."; return null; }

            try
            {
                var folder = game.InstallPath;
                var bat = Path.Combine(folder, "run.bat");
                string toLaunch = null;
                if (File.Exists(bat)) toLaunch = bat;
                else
                {
                    var exes = Directory.GetFiles(folder, "*.exe");
                    if (exes.Length > 0) toLaunch = exes[0];
                }

                if (toLaunch == null)
                {
                    error = "No se encontró run.bat ni .exe en la carpeta de instalación.";
                    return null;
                }

                var psi = new ProcessStartInfo
                {
                    FileName = toLaunch,
                    WorkingDirectory = folder,
                    UseShellExecute = true
                };

                var proc = Process.Start(psi);
                return proc;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public bool RecordPlaySession(int userId, int gameId, DateTime start, DateTime end, int durationSeconds)
        {
            if (userId <= 0 || gameId <= 0) return false;
            return _gameDAL.RecordPlaySession(userId, gameId, start, end, durationSeconds);
        }

        public TimeSpan GetTotalPlayTime(int userId, int gameId)
        {
            if (userId <= 0 || gameId <= 0) return TimeSpan.Zero;
            return _gameDAL.GetTotalPlayTimeForUserGame(userId, gameId);
        }

        // Wrappers de UserGames
        public bool AddGameToUser(User user, int gameId, string installPath)
        {
            if (user == null) return false;
            return _gameDAL.AddGameToUser(user.Id, gameId, installPath);
        }
        public bool RemoveGameFromUser(User user, int gameId) => user != null && _gameDAL.RemoveGameFromUser(user.Id, gameId);
        public bool UserOwnsGame(User user, int gameId) => user != null && _gameDAL.UserOwnsGame(user.Id, gameId);
        public bool UpdateUserGameInstallPath(User user, int gameId, string newInstallPath)
        {
            if (user == null) return false;
            return _gameDAL.UpdateUserGameInstallPath(user.Id, gameId, newInstallPath);
        }

    }

}

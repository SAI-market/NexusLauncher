using NexusLauncher.Models;
using NexusLauncher.BLL;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NexusLauncher.UI
{
    public partial class frmOperarJuego : Form
    {
        public Game CurrentGame { get; set; }

        private readonly GameService _gameService = new GameService();

        public frmOperarJuego()
        {
            InitializeComponent();
            this.Load += FrmOperarJuego_Load;
        }

        private void FrmOperarJuego_Load(object sender, EventArgs e)
        {
            PopulateFromCurrentGame();
        }

        /// <summary>
        /// Refresca la UI a partir de CurrentGame y habilita/deshabilita botones.
        /// Controles esperados (ajusta si tus nombres son distintos):
        ///   lblTitle, lblInstallPath, chkIsInstalled, pictureBoxCover,
        ///   btn_Instalar, btn_ChangePath, btn_Desinstalar, btn_Jugar
        /// </summary>
        private void PopulateFromCurrentGame()
        {
            if (CurrentGame == null)
            {
                if (this.Controls["lblTitle"] is Label lbl0) lbl0.Text = "No hay juego seleccionado";
                if (this.Controls["lblInstallPath"] is Label lblPath0) lblPath0.Text = "-";
                if (this.Controls["chkIsInstalled"] is CheckBox chk0) chk0.Checked = false;
                SetButtonsEnabled(false, false, false, false);
                if (this.Controls["pictureBoxCover"] is PictureBox pb0) pb0.Image = null;
                return;
            }

            if (this.Controls["lblTitle"] is Label lblTitle) lblTitle.Text = CurrentGame.Title ?? "—";
            if (this.Controls["lblInstallPath"] is Label lblInstallPath) lblInstallPath.Text = string.IsNullOrWhiteSpace(CurrentGame.InstallPath) ? "(no instalado)" : CurrentGame.InstallPath;
            if (this.Controls["chkIsInstalled"] is CheckBox chkIsInstalled) chkIsInstalled.Checked = CurrentGame.IsInstalled;

            // Cargar imagen si existe
            if (this.Controls["pictureBoxCover"] is PictureBox pictureBoxCover)
            {
                if (CurrentGame.Image != null && CurrentGame.Image.Length > 0)
                {
                    try
                    {
                        using (var ms = new MemoryStream(CurrentGame.Image))
                        {
                            var img = Image.FromStream(ms);
                            pictureBoxCover.Image = img;
                        }
                    }
                    catch
                    {
                        pictureBoxCover.Image = null;
                    }
                }
                else
                {
                    pictureBoxCover.Image = null;
                }
            }

            bool hasPath = !string.IsNullOrWhiteSpace(CurrentGame.InstallPath) && Directory.Exists(CurrentGame.InstallPath);

            // instalar habilitado sólo si no hay ruta; changePath/desinstalar/jugar habilitados solo si hay ruta existente
            SetButtonsEnabled(!hasPath, hasPath, hasPath, hasPath);
        }

        private void SetButtonsEnabled(bool instalar, bool changePath, bool desinstalar, bool jugar)
        {
            if (this.Controls["btn_Instalar"] is Button btnInst) btnInst.Enabled = instalar;
            if (this.Controls["btn_ChangePath"] is Button btnCh) btnCh.Enabled = changePath;
            if (this.Controls["btn_Desinstalar"] is Button btnDes) btnDes.Enabled = desinstalar;
            if (this.Controls["btn_Jugar"] is Button btnPlay) btnPlay.Enabled = jugar;
        }

        private void btn_Instalar_Click(object sender, EventArgs e)
        {
            if (CurrentGame == null)
            {
                MessageBox.Show("Selecciona un juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sólo si no hay ruta o carpeta no existe
            if (!string.IsNullOrWhiteSpace(CurrentGame.InstallPath) && Directory.Exists(CurrentGame.InstallPath))
            {
                MessageBox.Show("El juego ya tiene una ruta de instalación.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateFromCurrentGame();
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleccione carpeta base para instalar el juego";
                fbd.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "NexusLauncher", "Games");
                if (fbd.ShowDialog() != DialogResult.OK) return;

                string baseInstallRoot = fbd.SelectedPath;

                string err;
                var ok = _gameService.InstallAndRegisterGameForUser(SessionManager.Instance.CurrentUser, CurrentGame, baseInstallRoot, out err);
                if (!ok)
                {
                    MessageBox.Show("No se pudo instalar: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PopulateFromCurrentGame();
                    return;
                }

                MessageBox.Show("Instalación completada", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateFromCurrentGame();
            }
        }

        private void btn_ChangePath_Click(object sender, EventArgs e)
        {
            if (CurrentGame == null)
            {
                MessageBox.Show("Selecciona un juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(CurrentGame.InstallPath) || !Directory.Exists(CurrentGame.InstallPath))
            {
                MessageBox.Show("El juego no tiene una instalación válida para mover. Usa Instalar en su lugar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateFromCurrentGame();
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleccione la nueva carpeta base para el juego";
                if (fbd.ShowDialog() != DialogResult.OK) return;

                string newBase = fbd.SelectedPath;

                // safeName: eliminar caracteres inválidos
                var invalidChars = Path.GetInvalidFileNameChars();
                var safeName = new string((CurrentGame.Title ?? $"game_{CurrentGame.Id}").Where(c => !invalidChars.Contains(c)).ToArray());
                if (string.IsNullOrWhiteSpace(safeName)) safeName = $"game_{CurrentGame.Id}";

                string oldFolder = CurrentGame.InstallPath;
                string newFolder = Path.Combine(newBase, safeName);

                try
                {
                    // Crear nueva carpeta
                    Directory.CreateDirectory(newFolder);

                    bool success = false;

                    // Intentamos mover la carpeta (funciona en mismo volumen)
                    try
                    {
                        Directory.Move(oldFolder, newFolder);
                        success = true;
                    }
                    catch
                    {
                        // Si falla el move (por ej. distinto volumen), copiamos recursivamente
                        try
                        {
                            foreach (var file in Directory.GetFiles(oldFolder, "*", SearchOption.AllDirectories))
                            {
                                var relativePath = file.Substring(oldFolder.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                                var destFile = Path.Combine(newFolder, relativePath);
                                var destDir = Path.GetDirectoryName(destFile);
                                if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
                                File.Copy(file, destFile, true);
                            }
                            success = true;
                        }
                        catch (Exception exCopy)
                        {
                            MessageBox.Show("No se pudo mover/copiar los archivos: " + exCopy.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // limpiar carpeta nueva si algo quedó
                            try { if (Directory.Exists(newFolder)) Directory.Delete(newFolder, true); } catch { }
                            PopulateFromCurrentGame();
                            return;
                        }
                    }

                    // Si copiamos, intentamos borrar la carpeta vieja
                    if (success)
                    {
                        try
                        {
                            if (Directory.Exists(oldFolder) && !string.Equals(oldFolder.TrimEnd(Path.DirectorySeparatorChar), newFolder.TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
                                Directory.Delete(oldFolder, true);
                        }
                        catch
                        {
                            // no fatal
                        }
                    }

                    // Asegurar que exista un "ejecutable" (run.bat) o txt en la nueva carpeta. Si no existe, crear uno simple apuntando al txt.
                    var bat = Path.Combine(newFolder, "run.bat");
                    var txts = Directory.GetFiles(newFolder, "*.txt", SearchOption.TopDirectoryOnly);
                    if (!File.Exists(bat) && (txts == null || txts.Length == 0))
                    {
                        // crear archivo txt y bat de emergencia
                        var txtPath = Path.Combine(newFolder, safeName + ".txt");
                        File.WriteAllText(txtPath, $"Archivo creado al mover el juego {CurrentGame.Title}{Environment.NewLine}Fecha: {DateTime.Now}", Encoding.UTF8);

                        var sb = new StringBuilder();
                        sb.AppendLine("@echo off");
                        sb.AppendLine($"start \"\" \"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe")}\" \"{txtPath}\"");
                        sb.AppendLine("exit");
                        File.WriteAllText(bat, sb.ToString(), Encoding.ASCII);
                    }

                    // Actualizar DB: guardamos la nueva ruta para el usuario
                    bool updOk = _gameService.UpdateUserGameInstallPath(SessionManager.Instance.CurrentUser, CurrentGame.Id, newFolder);
                    if (!updOk)
                    {
                        MessageBox.Show("Se cambió la ruta localmente pero no se pudo actualizar en la base de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Actualizar objeto y UI
                    CurrentGame.InstallPath = newFolder;
                    CurrentGame.IsInstalled = true;

                    MessageBox.Show("Ruta cambiada con éxito.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateFromCurrentGame();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cambiar ruta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PopulateFromCurrentGame();
                }
            }
        }

        private void btn_Desinstalar_Click(object sender, EventArgs e)
        {
            if (CurrentGame == null)
            {
                MessageBox.Show("Selecciona un juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(CurrentGame.InstallPath) || !Directory.Exists(CurrentGame.InstallPath))
            {
                MessageBox.Show("El juego no está instalado o la ruta no existe.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateFromCurrentGame();
                return;
            }

            var r = MessageBox.Show("¿Desea desinstalar el juego y eliminar los archivos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            string folderToDelete = CurrentGame.InstallPath;

            try
            {
                Directory.Delete(folderToDelete, true);
            }
            catch
            {
                // Si falla el borrado directo, intentamos borrar archivos y carpetas de forma resiliente
                try
                {
                    foreach (var file in Directory.GetFiles(folderToDelete, "*", SearchOption.AllDirectories))
                    {
                        try { File.Delete(file); } catch { }
                    }

                    var dirs = Directory.GetDirectories(folderToDelete, "*", SearchOption.AllDirectories)
                                        .OrderByDescending(d => d.Length)
                                        .ToArray();
                    foreach (var dir in dirs)
                    {
                        try { Directory.Delete(dir, true); } catch { }
                    }

                    try { if (Directory.Exists(folderToDelete)) Directory.Delete(folderToDelete, true); } catch { }
                }
                catch
                {
                    // swallow
                }
            }

            // Actualizar BD: quitar InstallPath para este usuario
            var updOk = _gameService.UpdateUserGameInstallPath(SessionManager.Instance.CurrentUser, CurrentGame.Id, null);
            if (!updOk)
            {
                MessageBox.Show("No se pudo actualizar la base de datos al desinstalar (InstallPath).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            CurrentGame.InstallPath = null;
            CurrentGame.IsInstalled = false;

            MessageBox.Show("Desinstalado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PopulateFromCurrentGame();
        }

        private void btn_Jugar_Click(object sender, EventArgs e)
        {
            if (CurrentGame == null)
            {
                MessageBox.Show("Selecciona un juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(CurrentGame.InstallPath) || !Directory.Exists(CurrentGame.InstallPath))
            {
                MessageBox.Show("Ruta de instalación no encontrada. Instale o cambie la ruta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PopulateFromCurrentGame();
                return;
            }

            // Lanzamos el proceso
            var proc = _gameService.LaunchGameProcess(CurrentGame, out string err);
            if (proc == null)
            {
                MessageBox.Show("No se pudo iniciar: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Actualizar estado en la sesión ANTES de notificar al frmMain
            var sessionUser = SessionManager.Instance.CurrentUser;
            if (sessionUser != null)
            {
                sessionUser.CurrentStatus = $"Jugando: \"{CurrentGame.Title}\"";
            }

            // Referencia segura al frmMain propietario (si fue abierto con ShowDialog(this) desde frmMain)
            var mainOwner = this.Owner as frmMain;
            if (mainOwner != null)
            {
                try { mainOwner.SetPlaying(CurrentGame.Title); } catch { /* no fatal */ }
            }

            DateTime start = DateTime.Now;
            var currentUser = SessionManager.Instance.CurrentUser;
            proc.EnableRaisingEvents = true;
            proc.Exited += (s, ev) =>
            {
                try
                {
                    DateTime end = DateTime.Now;
                    int dur = (int)(end - start).TotalSeconds;
                    if (currentUser != null)
                    {
                        _gameService.RecordPlaySession(currentUser.Id, CurrentGame.Id, start, end, dur);
                    }
                }
                catch
                {
                    // swallow
                }
                finally
                {
                    // Restaurar estado de sesión y UI a Online cuando el proceso termine
                    if (sessionUser != null)
                    {
                        sessionUser.CurrentStatus = "Online";
                    }

                    if (mainOwner != null)
                    {
                        try { mainOwner.SetOnline(); } catch { /* swallow */ }
                    }
                }
            };

            // Marcar instalado y cerrar (para que frmMain recargue si lo necesita)
            CurrentGame.IsInstalled = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}

using NexusLauncher.DAL;
using NexusLauncher.Models;

namespace NexusLauncher.BLL
{
    public class UserService
    {
        private readonly UserDAL _userDal = new UserDAL();

        // Valida usuario y contraseña ;D
        public bool Authenticate(string username, string password, out User user)
        {
            user = _userDal.GetByUsername(username);

            if (user == null)
                return false;

            // Por ahora comparamos directo (más adelante se puede usar hash de contraseña)
            if (user.Password == password)
                return true;

            user = null;
            return false;
        }
    }
}

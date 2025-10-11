using System.Configuration;

namespace NexusLauncher.DAL
{
    public static class DbConfig
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["NexusLauncherDB"].ConnectionString;
    }
}
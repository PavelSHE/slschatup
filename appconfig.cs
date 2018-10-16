using System.IO;

namespace Arc.Function
{
    public static class appconfig
    {
        private static string configurationVariable(string name)
            => System.Environment.GetEnvironmentVariable(name);      
        public static string home = configurationVariable("HOME");
        public static string publicFolder = configurationVariable("PUBLICFOLDER");
        public static string defaultFile = configurationVariable("DEFAULTFILE");
        public static string basePath = Path.Combine(home,publicFolder);
    }
}
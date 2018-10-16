using System.IO;

namespace Arc.Function
{
    public static class appconfig
    {    
        public static string home = HelpFunctions.configurationVariable("HOME");
        public static string publicFolder = HelpFunctions.configurationVariable("PUBLICFOLDER");
        public static string defaultFile = HelpFunctions.configurationVariable("DEFAULTFILE");
        public static string basePath = Path.Combine(home,publicFolder);
    }
}
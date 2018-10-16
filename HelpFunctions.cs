using System.Linq;
using MimeTypes;
using System.IO;

namespace Arc.Function
{
    public static class HelpFunctions
    {
        public static string configurationVariable(string name)
            => System.Environment.GetEnvironmentVariable(name);
        public static string FirstNonEmpty(string[] arr){
            return arr.FirstOrDefault(s => !string.IsNullOrEmpty(s)) ?? "";
        }

        public static string GetMimeType(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return MimeTypeMap.GetMimeType(fileInfo.Extension);
        }

        public static byte[] ReadStreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16*1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static byte[] GetFile(string localPath){
            FileStream stream = File.OpenRead(localPath);
            return ReadStreamToByteArray(stream);
        }
    }
}
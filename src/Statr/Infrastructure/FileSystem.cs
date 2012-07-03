using System.IO;

namespace Statr.Infrastructure
{
    public class FileSystem : IFileSystem
    {
        public StreamReader OpenText(string fileName)
        {
            return File.OpenText(fileName);
        }

        public void WriteText(string fileName, string content)
        {
            File.WriteAllText(fileName, content);
        }
    }
}
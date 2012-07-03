using System.IO;

namespace Statr.Infrastructure
{
    public interface IFileSystem
    {
        StreamReader OpenText(string fileName);

        void WriteText(string fileName, string content);
    }
}

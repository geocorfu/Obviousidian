using System.IO;
using System.Threading.Tasks;

namespace Obviousidian.Core.Services
{
    public class FileService : IFileService
    {
        public Task WriteTextAsync(string path, string content)
        {
            return File.WriteAllTextAsync(path, content);
        }

        public Task WriteBytesAsync(string path, byte[] bytes)
        {
            return File.WriteAllBytesAsync(path, bytes);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}

using System.Threading.Tasks;

namespace Obviousidian.Core.Services
{
    public interface IFileService
    {
        Task WriteTextAsync(string path, string content);
        Task WriteBytesAsync(string path, byte[] bytes);
        bool DirectoryExists(string path);
        bool FileExists(string path);
        void CreateDirectory(string path);
    }
}

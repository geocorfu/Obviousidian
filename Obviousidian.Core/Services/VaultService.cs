using System;
using System.IO;

namespace Obviousidian.Core.Services
{
    public class VaultService
    {
        private readonly string _vaultPath;
        private readonly IFileService _fileService;

        public VaultService(string vaultPath, IFileService fileService)
        {
            _vaultPath = vaultPath;
            _fileService = fileService;
            EnsureDirectories();
        }

        private void EnsureDirectories()
        {
            string[] folders = { "inbox", "notes", "bookmarks", "articles", "videos", "screenshots", "attachments" };
            foreach (var folder in folders)
            {
                var path = Path.Combine(_vaultPath, folder);
                if (!_fileService.DirectoryExists(path))
                {
                    _fileService.CreateDirectory(path);
                }
            }
        }

        public string GetPathFor(string subfolder, string filename)
        {
            return Path.Combine(_vaultPath, subfolder, filename);
        }

        public string GetDefaultFolderForText() => "inbox";
    }
}

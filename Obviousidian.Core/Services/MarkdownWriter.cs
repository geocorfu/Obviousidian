using System;
using System.Threading.Tasks;

namespace Obviousidian.Core.Services
{
    public class MarkdownWriter
    {
        private readonly VaultService _vaultService;
        private readonly IFileService _fileService;

        public MarkdownWriter(VaultService vaultService, IFileService fileService)
        {
            _vaultService = vaultService;
            _fileService = fileService;
        }

        public async Task SaveTextNoteAsync(string content, string title = null, string targetFolder = null)
        {
            var fileName = title ?? $"Note {DateTime.Now:yyyy-MM-dd HH-mm-ss}";
            // Sanitize filename
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            fileName += ".md";

            string folder = targetFolder ?? _vaultService.GetDefaultFolderForText();
            string fullPath = _vaultService.GetPathFor(folder, fileName);

            // Generate Frontmatter
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("---");
            sb.AppendLine($"created_at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine("source: \"manual\"");
            sb.AppendLine("tags: []");
            sb.AppendLine("---");
            sb.AppendLine();
            sb.Append(content);

            await _fileService.WriteTextAsync(fullPath, sb.ToString());
        }

        public async Task SaveImageNoteAsync(byte[] imageBytes, string title = null)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string imageFileName = $"Img_{timestamp}.png";
            string noteTitle = title ?? $"Screenshot {DateTime.Now:yyyy-MM-dd HH-mm-ss}";
            string noteFileName = $"{noteTitle}.md";
            
            // 1. Save Image to Attachments
            string attachmentsFolder = "attachments";
            string imagePath = _vaultService.GetPathFor(attachmentsFolder, imageFileName);
            await _fileService.WriteBytesAsync(imagePath, imageBytes);

            // 2. Create Note in Screenshots
            string screenshotsFolder = "screenshots";
            string notePath = _vaultService.GetPathFor(screenshotsFolder, noteFileName);
            
            // Obsidian syntax for image embedding
            string content = $"![[{imageFileName}]]\n\nCaptured: {DateTime.Now}";
            
            await _fileService.WriteTextAsync(notePath, content);
        }

        public async Task SaveUrlNoteAsync(string url, string title, string category)
        {
            string cleanTitle = title;
            if (string.IsNullOrWhiteSpace(cleanTitle))
            {
                cleanTitle = "Untitled Link";
            }

            // Sanitize filename
            string fileName = cleanTitle;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            // Truncate if too long (max 50 chars for filename)
            if (fileName.Length > 50) fileName = fileName.Substring(0, 50);
            
            fileName += ".md";

            string fullPath = _vaultService.GetPathFor(category, fileName);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("---");
            sb.AppendLine($"created_at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine("source: \"url\"");
            sb.AppendLine($"url: \"{url}\"");
            sb.AppendLine("tags: []");
            sb.AppendLine("---");
            sb.AppendLine();
            sb.AppendLine($"# [{cleanTitle}]({url})");

            await _fileService.WriteTextAsync(fullPath, sb.ToString());
        }
    }
}

using System;

namespace Obviousidian.Core.Services
{
    public class CaptureRouter
    {
        private readonly VaultService _vaultService;

        public CaptureRouter(VaultService vaultService)
        {
            _vaultService = vaultService;
        }

        public (string Folder, string Type) RouteContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return ("inbox", "Text");

            if (IsUrl(content))
            {
                if (IsVideoUrl(content))
                    return ("videos", "Video");
                
                // Naive article check or just default to bookmarks
                return ("bookmarks", "Link");
            }

            return ("notes", "Text");
        }

        private bool IsUrl(string content)
        {
            return Uri.TryCreate(content, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private bool IsVideoUrl(string content)
        {
            var lower = content.ToLowerInvariant();
            return lower.Contains("youtube.com") || 
                   lower.Contains("youtu.be") || 
                   lower.Contains("vimeo.com");
        }
    }
}

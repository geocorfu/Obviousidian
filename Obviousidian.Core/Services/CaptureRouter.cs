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
                return ("notes", "Text");

            if (IsUrl(content))
            {
                if (IsVideoUrl(content))
                    return ("videos", "Video");
                
                if (IsArticleUrl(content))
                    return ("articles", "Article");

                return ("bookmarks", "Link");
            }

            return ("notes", "Text");
        }

        private bool IsUrl(string content)
        {
            // Simple check, can be improved
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

        private bool IsArticleUrl(string content)
        {
            // Heuristic: URL paths with significant depth often indicate articles
            // e.g. example.com/blog/my-post vs example.com
            // Also check for common article platforms
            
            var lower = content.ToLowerInvariant();
            if (lower.Contains("medium.com") || lower.Contains("dev.to") || lower.Contains("substack.com"))
                return true;

            if (Uri.TryCreate(content, UriKind.Absolute, out var uri))
            {
                // If path has more than 1 segment or length > 20, assume it might be specific content
                if (uri.Segments.Length > 2 || uri.PathAndQuery.Length > 20)
                    return true;
            }

            return false;
        }
    }
}

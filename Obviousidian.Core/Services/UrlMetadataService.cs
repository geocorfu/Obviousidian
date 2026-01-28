using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Obviousidian.Core.Services
{
    public class UrlMetadataService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> FetchTitleAsync(string url)
        {
            try
            {
                // Add a user agent to avoid being rejected by some servers
                if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
                {
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", "Obviousidian/1.0");
                }

                // Download only the first 20KB to find the title quickly
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    // Basic timeout
                    _httpClient.Timeout = TimeSpan.FromSeconds(5);
                    
                    using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        if (!response.IsSuccessStatusCode) return null;

                        var html = await response.Content.ReadAsStringAsync();
                        var match = Regex.Match(html, @"<title>\s*(.+?)\s*</title>", RegexOptions.IgnoreCase);
                        
                        if (match.Success)
                        {
                            // Decode HTML entities (e.g. &amp; -> &)
                             return System.Net.WebUtility.HtmlDecode(match.Groups[1].Value);
                        }
                    }
                }
            }
            catch
            {
                // Ignore download errors, just return null (title not found)
            }
            return null;
        }
    }
}

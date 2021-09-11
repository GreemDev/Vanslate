using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Gommon;

namespace Vanslate.Helpers
{
    public static class HttpHelper
    {
        /// <summary>
        ///     POSTs the string <paramref name="content"/> to https://paste.greemdev.net with the <see cref="HttpClient"/> from <paramref name="provider"/>.
        ///     This method will throw if there is no <see cref="HttpClient"/> in the <paramref name="provider"/> given.
        /// </summary>
        /// <param name="content">The content to send.</param>
        /// <param name="provider">The <see cref="IServiceProvider"/> containing the <see cref="HttpClient"/>.</param>
        /// <param name="fileExtension">The file extension {WITHOUT PRECEDING PERIOD} for the resulting URL.</param>
        /// <returns>The URL of the successful paste.</returns>
        /// <exception cref="InvalidOperationException">If <paramref name="provider"/> doesn't have an <see cref="HttpClient"/> in it.</exception>
        public static async Task<string> PostToGreemPasteAsync(string content, IServiceProvider provider, string fileExtension = null)
        {
            try
            {
                var jdoc = JsonDocument.Parse(await (await PostStringAsync(provider, "https://paste.greemdev.net/documents", content)).Content.ReadAsStringAsync());
                return $"https://paste.greemdev.net/{jdoc.RootElement.GetProperty("key").GetString()}{(fileExtension is null ? "" : $".{fileExtension}")}}}";
            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary>
        ///     POSTs the specified string <paramref name="content"/> to <paramref name="url"/> with the <see cref="HttpClient"/> from <paramref name="provider"/>.
        /// </summary>
        /// <param name="provider">The <see cref="IServiceProvider"/> containing the <see cref="HttpClient"/>.</param>
        /// <param name="url">The URL to POST to.</param>
        /// <param name="content">The content to POST.</param>
        /// <returns>The resulting <see cref="HttpResponseMessage"/>.</returns>
        public static Task<HttpResponseMessage> PostStringAsync(IServiceProvider provider, string url, string content) 
            => provider.Get<HttpClient>().PostAsync(url, new StringContent(content, Encoding.UTF8, "text/plain"));
    }
}
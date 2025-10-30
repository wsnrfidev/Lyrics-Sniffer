using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CliLyricsSniffer.Core.Utils;

namespace CliLyricsSniffer.Core.Services
{
    public static class SpotifyAuth
    {
        private const string Scope = "user-read-playback-state";
        private const string TokenUrl = "https://accounts.spotify.com/api/token";
        private const string AuthUrl = "https://accounts.spotify.com/authorize";

        public static async Task<string?> AuthorizeAsync(string clientId, string redirectUri)
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(redirectUri))
            {
                Theme.Warn("⚠️ Missing ClientId or RedirectUri!");
                return null;
            }

            string codeVerifier = Base64Url(RandomBytes(64));
            string codeChallenge = Base64Url(SHA256(Encoding.ASCII.GetBytes(codeVerifier)));

            string url =
                $"{AuthUrl}?response_type=code&client_id={Uri.EscapeDataString(clientId)}" +
                $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                $"&code_challenge_method=S256&code_challenge={codeChallenge}" +
                $"&scope={Uri.EscapeDataString(Scope)}";

            var listener = new HttpListener();
            listener.Prefixes.Add(redirectUri + "/");
            listener.Start();

            try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
            catch { Console.WriteLine($"Open this URL:\n{url}"); }

            var context = await listener.GetContextAsync();
            string code = context.Request.QueryString["code"];

            byte[] msg = Encoding.UTF8.GetBytes("<h2>✅ Authorized! You can close this.</h2>");
            context.Response.ContentLength64 = msg.Length;
            await context.Response.OutputStream.WriteAsync(msg, 0, msg.Length);
            context.Response.Close();
            listener.Stop();

            if (string.IsNullOrWhiteSpace(code)) return null;

            using var http = new HttpClient();
            var form = new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["grant_type"] = "authorization_code",
                ["code"] = code!,
                ["redirect_uri"] = redirectUri,
                ["code_verifier"] = codeVerifier
            };

            var res = await http.PostAsync(TokenUrl, new FormUrlEncodedContent(form));
            var json = await res.Content.ReadAsStringAsync();
            var jo = JObject.Parse(json);

            return jo["access_token"]?.ToString();
        }

        static byte[] RandomBytes(int len)
        {
            var b = new byte[len];
            RandomNumberGenerator.Create().GetBytes(b);
            return b;
        }

        static byte[] SHA256(byte[] data)
            => System.Security.Cryptography.SHA256.Create().ComputeHash(data);

        static string Base64Url(byte[] b)
            => Convert.ToBase64String(b).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
}

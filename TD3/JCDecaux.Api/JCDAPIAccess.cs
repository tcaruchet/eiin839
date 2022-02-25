using System;
using System.Net.Http;

namespace JCDecaux.Api
{
    public static class JCDAPIAccess
    {
        public static readonly HttpClient Client = new HttpClient();

        public static readonly string Url = "https://api.jcdecaux.com/vls/v3/";
        public static readonly string ApiKey = "47ff5058c14817b9c348ab9622c16f96fe818454";

        public static string GetUrl(string parameters)
        {
            return Url + parameters + $"{(parameters.Contains("?") ? "&" : "?")}apiKey={ApiKey}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JCDecauxTest.Contracts
{
    internal class Program
    {
        static readonly HttpClient Client = new HttpClient();

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine(JsonConvert.DeserializeObject<object>(await Client.GetStringAsync(JCDecaux.Api.JCDAPIAccess.GetUrl("contracts"))));
            }
            catch (HttpRequestException e)
            {
                if (e != null && (e is HttpRequestException || e is JsonSerializationException))
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            Console.ReadKey();
        }
    }
}

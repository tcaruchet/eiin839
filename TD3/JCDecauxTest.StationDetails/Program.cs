using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxTest.StationDetails
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine(await JCDecaux.Api.JCDController.GetStationDetails(args.Length > 0 ? args[0] : string.Empty, Convert.ToInt32(args.Length > 1 ? args[1] : string.Empty)));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.ReadKey();
        }
    }
}

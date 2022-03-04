using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JCDecaux.Api.Models;

namespace JCDecauxTest.NearestStation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Position position = new Position(Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));
                Console.WriteLine(await JCDecaux.Api.JCDController.GetStationNearest(args.Length > 0 ? args[0] : string.Empty, position));
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

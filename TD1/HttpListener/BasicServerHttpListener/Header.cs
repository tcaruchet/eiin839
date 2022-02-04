using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Header
    {
        internal static void ShowHeaders(HttpListenerRequest req)
        {
            //foreach (var header in req.Headers)
            //{
            //    Console.WriteLine($"{header} : {req.Headers.Get(header.ToString())}");
            //}
            Console.WriteLine($"Connection : {req.Headers.Get("Connection")}");
            Console.WriteLine($"Accept : {req.Headers.Get("Accept")}");
            Console.WriteLine($"Accept-Encoding : {req.Headers.Get("Accept-Encoding")}");
            Console.WriteLine($"Host : {req.Headers.Get("Host")}");
            Console.WriteLine($"User-Agent : {req.Headers.Get("User-Agent")}");
            Console.WriteLine(req.HttpMethod);
            Console.WriteLine(Environment.NewLine);
        }
    }
}

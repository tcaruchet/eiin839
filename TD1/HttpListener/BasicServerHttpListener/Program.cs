using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using ConsoleApp3;

namespace BasicServerHTTPlistener
{
    internal class Program
    {
        private static HttpListener _listener;
        private const string Url = "http://localhost:8080/";
        private static readonly int _pageViews = 0;
        private static int _requestCount = 0;
        private static string _pageData = "";

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await _listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                Console.WriteLine($"Request #: {++_requestCount}");
                Header.ShowHeaders(req);


                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(string.Format(_pageData, _pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }


        public static void Main(string[] args)
        {
            //CHARGE la page HTML statiquement
            var indexStream = File.OpenRead(@"D:\Polytech\SI4\S8\SOC\eiin839\TD1\HttpListener\BasicServerHttpListener\Views\index.html");
            using (var reader = new StreamReader(indexStream))
                _pageData = reader.ReadToEnd();

            // Create a Http server and start listening for incoming connections
            _listener = new HttpListener();
            _listener.Prefixes.Add(Url);
            _listener.Start();
            Console.WriteLine("Listening for connections on {0}", Url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            _listener.Close();
        }
    }
}
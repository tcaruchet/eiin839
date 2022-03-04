using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using BasicWebServer;
using BasicWebServer.Helpers;
using BasicWebServer.Parsers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BasicServerHTTPlistener
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(TCaruchet.Header); //Header
            Console.WriteLine(TCaruchet.Body);

            Console.WriteLine("\n\r TD2 noté - 04/03/2022");

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }
 
            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // netsh http add urlacl url=http://localhost:8080/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:8081/ user="Tout le monde"

                }
            }
            else
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            listener.Start();

            // get args 
            foreach (string s in args)
                Console.WriteLine("### Listening for connections on " + s);


            //#############################
            // METHODES DISPONIBLES
            //#############################

            Console.WriteLine("####### Methodes Disponibles");
            foreach (MethodInfo info in typeof(MyMethods).GetMethods())
                if(info.GetBaseDefinition().DeclaringType != typeof(object))
                    Console.WriteLine(info.ToString());



            // Catch Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Console.WriteLine("HTTP Listener successfully stopped !");
                Environment.Exit(0);
            };


            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                
                // get url 
                Console.WriteLine($"Received request for {request.Url}");

                //get url protocol
                Console.WriteLine($"Scheme : {request.Url.Scheme}");
                //get user in url
                Console.WriteLine($"UserInfo : {request.Url.UserInfo}");
                //get host in url
                Console.WriteLine($"Host : {request.Url.Host}");
                //get port in url
                Console.WriteLine($"Port : {request.Url.Port}");
                //get path in url 
                Console.WriteLine($"LocalPath : {request.Url.LocalPath}");

                // parse path in url 
                foreach (string str in request.Url.Segments)
                    Console.WriteLine(str);

                //get params un url. After ? and between &
                Console.WriteLine($"Query : {request.Url.Query}");

                //parse params in url
                NameValueCollection queriesCollection = HttpUtility.ParseQueryString(request.Url.Query);
                foreach (string key in queriesCollection.AllKeys)
                    Console.WriteLine(queriesCollection.Get(key));
                
                Console.WriteLine($"DocumentContent : {documentContents}");

                // Obtain a response object.
                HttpListenerResponse response = context.Response;


                string methodCalled = request.Url.LocalPath.Replace("/", "");
                // Construct a response.
                Type type = typeof(MyMethods);
                MethodInfo method = type.GetMethods().FirstOrDefault(m =>
                    m.Name.Equals(methodCalled, StringComparison.InvariantCultureIgnoreCase));
                MyMethods c = new MyMethods();

                string jsonResponse;

                try
                {
                    if (method != null)
                    {
                        jsonResponse = TCJsonParser.Content(method.Invoke(c, new object[] { request.QueryString }).ToString());
                        if (string.IsNullOrWhiteSpace(jsonResponse))
                        {
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            jsonResponse = TCJsonParser.Empty;
                        }
                    }
                    else
                    {
                        jsonResponse = TCJsonParser.Message($"Uanble to find method {methodCalled}");
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    jsonResponse = TCJsonParser.Exception(ex.InnerException ?? ex); //Except WatsonBuckets
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonResponse);
                // Get a response stream and write the response to it
                response.ContentLength64 = buffer.Length;
                response.ContentType = jsonResponse.ContainsIgnoreCase("html") ? "text/html" : "application/json; charset=utf-8"; 
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // close the output stream.
                output.Close();
            }
            // Httplistener never stop ... Ctrl-C ...
            // listener.Stop();
        }
    }
}
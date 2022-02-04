﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace BasicServerHTTPlistener
{
    internal class OldProgram
    {
        private static void OldMain(string[] args)
        {
            

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }

            // Create a listener.
            var listener = new HttpListener();

            // Trap Ctrl-C and exit 
            Console.CancelKeyPress += delegate
            {
                listener.Stop();
                System.Environment.Exit(0);
            };

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (var s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();
            foreach (var s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                var context = listener.GetContext();
                var request = context.Request;

                string documentContents;
                using (var receiveStream = request.InputStream)
                {
                    using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                Console.WriteLine($"Received request for {request.Url}");
                Console.WriteLine(documentContents);

                // Obtain a response object.
                var response = context.Response;

                // Construct a response.
                FileStream indexStream = File.OpenRead(@"D:\Polytech\SI4\S8\SOC\eiin839\TD1\HttpListener\BasicServerHttpListener\Views\index.html");
                string responseString;
                using (StreamReader reader = new StreamReader(indexStream))
                    responseString = reader.ReadToEnd();
                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                var output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            // Httplistener neither stop ...
            // listener.Stop();
        }
    }
}
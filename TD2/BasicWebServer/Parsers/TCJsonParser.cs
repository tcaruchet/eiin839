using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BasicWebServer.Parsers
{
    internal class TCJsonParser
    {
        public static readonly string Empty = "{}";

        public static string Content(object o)
        {
            return JObject.FromObject(new
            {
                content = o
            }).ToString();
        }

        public static string Message(object o)
        {
            return JObject.FromObject(new
            {
                message = o
            }).ToString();
        }

        public static string Exception(Exception ex)
        {
            return JObject.FromObject(new
            {
                ClassName = ex.GetType().Name,
                ex.Message,
                ex.StackTrace,
                ex.InnerException
            }).ToString();
        }
    }
}

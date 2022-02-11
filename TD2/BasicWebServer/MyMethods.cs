using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer
{
    public class MyMethods
    {
        public string Home()
        {
            return "<HTML><BODY> Hello world!</BODY></HTML>";
        }

        public string SuperReflexion(NameValueCollection paramsCollection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><BODY> Hello ");
            foreach (var key in paramsCollection.AllKeys)
                sb.Append($"{paramsCollection.Get(key)} et ");
            sb.Append(" </BODY></HTML>");
            return sb.ToString();
        }
    }
}

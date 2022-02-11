using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
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

        public string ExternalExeCall(NameValueCollection paramsCollection)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"D:\Polytech\SI4\S8\SOC\eiin839\TD2\ExecTest\bin\Debug\ExecTest.exe"; 
            start.Arguments = String.Join(",", paramsCollection.Cast<string>().Select(s => paramsCollection[s])); 
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            string result = "";
            using (Process process = Process.Start(start))
                if(process != null)
                    using (StreamReader reader = process.StandardOutput)
                        result = reader.ReadToEnd();
            return result;
        }
    }
}

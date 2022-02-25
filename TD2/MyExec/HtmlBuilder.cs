using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MyExec
{
    public class HtmlBuilder
    {
        private List<H1> h1 = new List<H1>();

        public HtmlBuilder(H1 h1)
        {
            this.h1.Add(h1);
        }

        public HtmlBuilder(List<H1> h1)
        {
            this.h1.AddRange(h1);
        }

        public void Write(TextWriter writer, HtmlEncoder encoder)
        {
            var builder = new HtmlContentBuilder();
            List<string> h1str = h1.Select(e => e.ToString().ToString()).ToList();
            builder.AppendFormat("<html><HEADER><title>My SOC Web Page !</title></HEADER><BODY>{0}</BODY></html>", String.Join(" <br> ", h1str));
            builder.WriteTo(writer, encoder);
        }
    }

    public class H1
    {
        public H1(string content)
        {
            Content = content;
        }

        private string Content { get; set; }

        public IHtmlContent ToString()
        {
            return new HtmlString($"<h1>{Content}</h1>");
        }
    }
}

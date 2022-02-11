﻿using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using MyExec;
using static System.Console;

if (args.Length == 1)
{
    List<H1> h1s = new List<H1>();
    foreach (var str in args[0].Split())
        h1s.Add(new H1(str));
    new HtmlBuilder(h1s).Write(Out, HtmlEncoder.Default);
    
}
else
    Console.WriteLine("MyExec <string parameter>");
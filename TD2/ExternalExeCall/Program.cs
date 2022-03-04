﻿using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = @"D:\Polytech\SI4\S8\SOC\eiin839\TD2\ExecTest\bin\Debug\ExecTest.exe"; // CHANGE THIS.
        start.Arguments = "Argument1"; // Specify arguments.
        start.UseShellExecute = false; 
        start.RedirectStandardOutput = true;
        // Start process
        using (Process process = Process.Start(start))
        {
            // Read in all the text from the process with the StreamReader.
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
    }
}
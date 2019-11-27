using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpNetCoreConsoleGlob
{
    class Program
    {
        static List<String> allDirectory = new List<string>();
        static void Main(string[] args)
        {
            //  string path = @"C:\Users\deno\Desktop\**\mobidick.dll";
            // string path = @"C:\Users\deno\Desktop\test\test\test1\test1.3\test1.3.1\asd.dll";
            string path =args[0];
            string extention = "";
            string fileName = "";
            if (path.Split("\\")[path.Split("\\").Length - 1].Contains("*"))
            {
                extention = "." + path.Split("\\")[path.Split("\\").Length - 1].Split(".")[1];
            }
            else
            {
                fileName = path.Split("\\")[path.Split("\\").Length - 1];
            }

            if (path.Contains("**"))
            {
                string[] splintPath = path.Split("**");
                GetSubDirectories(splintPath[0]);
                foreach (String direktory in allDirectory)
                {
                    WriteConsoleFile(direktory, extention, fileName);

                }
            }
            else {
                WriteConsoleFile(path.Substring(0,path.Length-path.Split("\\")[path.Split("\\").Length-1].Length), extention, fileName);
            }

        }

        static List<String> GetSubDirectories(string path)
        {
            List<String> directoryies = Directory.GetDirectories(path).OfType<String>().ToList();
            foreach (String directory in directoryies)
            {
                allDirectory.AddRange(GetSubDirectories(directory));
            }
            return directoryies;
           
        }

        static void WriteConsoleFile( string direktory,string extention,string fileName) {
            foreach (string file in GetFiles(direktory))
            {
                if (!extention.Equals(""))
                {
                    if (Path.GetExtension(file).Equals(extention))
                    {
                        Console.WriteLine(file);
                    }
                }
                else
                {

                    if (Path.GetFileName(file).Equals(fileName))
                    {
                        Console.WriteLine(file);
                    }

                }
            }
        }

        static List<String> GetFiles(String path)
        {
            List<String> files = Directory.GetFiles(path).OfType<String>().ToList();
            return files;
        }

    }
}

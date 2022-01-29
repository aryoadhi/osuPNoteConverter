﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace osuPNoteConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filedir;
            string Convert(string line)
            {
                string[] segments = line.Split(',');
                string[] segmentsSub = segments[segments.Length - 1].Split(':');
                string output = line;
                if (segmentsSub.Length == 5)
                {
                    string final = segments[5];
                    string time = segments[2];
                    segments[2] = "NaN";
                    segments[3] = "128";
                    final = $":{final}";
                    segments = segments.Reverse().Skip(1).Reverse().ToArray();
                    output = String.Join(",", segments);
                    output = $"{output},{time}{final}";
                }
                return output;
            }
            Console.Write("Insert filename here: ");
            filedir = Console.ReadLine();
            filedir = Directory.GetCurrentDirectory() + @"\" + filedir;
            var fileStream = new FileStream(filedir, FileMode.Open, FileAccess.Read);
            File.Copy(filedir, filedir + ".backup", true);
            string[] lines;
            var list = new List<string>();

            //adding lines to a LIST
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            lines = list.ToArray();
            //reading the LIST
            var listout = new List<string>();
            foreach (var item in lines)
            {
                Console.WriteLine(Convert(item));
                listout.Add(Convert(item));
            }
            File.WriteAllText(filedir, string.Empty);
            string[] lineout = listout.ToArray();
            File.WriteAllLines(filedir, lineout);
            fileStream.Close();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

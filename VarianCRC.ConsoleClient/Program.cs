﻿using System;
using System.IO;

namespace VarianCRC.ConsoleClient
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: crc <file_path>");
                return 1;
            }

            string filePath = args[0];
            byte[] fileContents = null;

            try
            {
                fileContents = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem reading the file.");
                Console.WriteLine(e.Message);
                return 1;
            }

            Console.WriteLine("{0:X4}", CRC.CalculateCRC(fileContents));

            return 0;
        }
    }
}

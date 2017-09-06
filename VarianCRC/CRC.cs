using System;
using System.IO;
using System.Text;

namespace VarianCRC
{
    public static class CRC
    {
        /// <summary>
        /// Add CRC at the end of a Varian MLC file
        /// </summary>
        /// <param name="filePath"> File path to the MLC file </param>
        public static void AddCrc(string filePath)
        {
            byte[] fileContents = null;

            try
            {
                fileContents = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem reading the file.");
                Console.WriteLine(e.Message);
            }

            ushort crc = CalculateCRC(fileContents);

            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                writer.WriteLine();
                writer.WriteLine("CRC = {0:X4}", crc);
            }
        }

        // Algorithm adapted from Varian MLC File Format
        // Description Reference Guide (July 2013)

        public static ushort CalculateCRC(string str)
        {
            ushort[] crcTable = BuildCRCTable();

            ushort crc = unchecked((ushort)~0);
            foreach (ushort c in str)
            {
                // Skip terminating characters
                if (c == (ushort)'\r' || c == (ushort)'\n')
                    continue;

                crc = (ushort)((crc << 8) ^ crcTable[(crc >> 8) ^ c]);
            }

            return crc;
        }

        public static ushort CalculateCRC(byte[] byteData)
        {
            ushort[] crcTable = BuildCRCTable();

            ushort crc = unchecked((ushort)~0);
            foreach (ushort c in byteData)
            {
                // Skip terminating characters
                if (c == (ushort)'\r' || c == (ushort)'\n')
                    continue;

                crc = (ushort)((crc << 8) ^ crcTable[(crc >> 8) ^ c]);
            }

            return crc;
        }

        private static ushort[] BuildCRCTable()
        {
            ushort[] crcTable = new ushort[256];

            ushort poly = 0x1021;
            for (int i = 0; i < 256; i++)
            {
                crcTable[i] = BuildCRCTableEntry((ushort)i, poly, 0);
            }

            return crcTable;
        }

        private static ushort BuildCRCTableEntry(ushort data, ushort poly, ushort acc)
        {
            data <<= 8;

            for (int i = 8; i > 0; i--)
            {
                if (((data ^ acc) & 0x8000) != 0)
                    acc = (ushort)((acc << 1) ^ poly);
                else
                    acc <<= 1;

                data <<= 1;
            }

            return acc;
        }

    }
}

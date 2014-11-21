using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VarianCRC
{
    public static class CRC
    {
        // Algorithm adapted from Varian MLC File Format
        // Description Reference Guide (July 2013)

        private static ushort CalculateCRC(string str)
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

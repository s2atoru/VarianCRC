# VarianCRC

This Visual Studio solution contains an implementation of the CRC
from the Varian MLC File Format Description Reference Guide (February 2014).

The VarianCRC project contains the CRC algorithm,
while the VarianCRC.ConsoleClient contains a console application
that outputs the CRC value of the given file.

Now the Varian MLC file contains the UTF-8 BOM at the beginning ot the file.
The CRC calculation includes the BOM.

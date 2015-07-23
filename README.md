# Crc32
Simple Crc32 HashAlgorithm implementation for .NET based on the forum post [here](http://sanity-free.org/12/crc32_implementation_in_csharp.html).


#Usage

Same usage as [HashAlgorithm](https://msdn.microsoft.com/en-us/library/vstudio/system.security.cryptography.hashalgorithm(v=vs.100).aspx) in .NET.

    var crc32 = Crc32.Create();
    var crcBytes = crc32.ComputeHash(Encoding.UTF8.GetBytes("hello, world"));
    var crcValue = BitConverter.ToUInt32(crcBytes, 0);


#Public Domain

This is free and unencumbered software released into the public domain.

See LICENSE for details.


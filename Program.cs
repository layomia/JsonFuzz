using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace utf8fuzz
{
    class Program
    {
        private static void TestNumberTryGetMethods(Utf8JsonReader reader)
        {
            Console.WriteLine("Running TryGetNumber tests.");

            string valueAsStr = Encoding.UTF8.GetString(reader.ValueSpan);

            if (!reader.TryGetByte(out _))
            {
                Console.WriteLine($"Failed to parse as Byte: {valueAsStr}");
            }

            if (!reader.TryGetSByte(out _))
            {
                Console.WriteLine($"Failed to parse as SByte: {valueAsStr}");
            }

            if (!reader.TryGetInt16(out _))
            {
                Console.WriteLine($"Failed to parse as Int16: {valueAsStr}");
            }

            if (!reader.TryGetInt32(out _))
            {
                Console.WriteLine($"Failed to parse as Int32: {valueAsStr}");
            }

            if (!reader.TryGetInt64(out _))
            {
                Console.WriteLine($"Failed to parse as Int64: {valueAsStr}");
            }

            if (!reader.TryGetUInt16(out _))
            {
                Console.WriteLine($"Failed to parse as UInt16: {valueAsStr}");
            }

            if (!reader.TryGetUInt32(out _))
            {
                Console.WriteLine($"Failed to parse as UInt32: {valueAsStr}");
            }

            if (!reader.TryGetUInt64(out _))
            {
                Console.WriteLine($"Failed to parse as UInt64: {valueAsStr}");
            }

            if (!reader.TryGetSingle(out _))
            {
                Console.WriteLine($"Failed to parse as Single: {valueAsStr}");
            }

            if (!reader.TryGetDouble(out _))
            {
                Console.WriteLine($"Failed to parse as Double: {valueAsStr}");
            }

            if (!reader.TryGetDecimal(out _))
            {
                Console.WriteLine($"Failed to parse as Decimal: {valueAsStr}");
            }
        }

        private static void TestNumberGetMethods(Utf8JsonReader reader)
        {
            Console.WriteLine("Running GetNumber tests.");
            string valueAsStr = Encoding.UTF8.GetString(reader.ValueSpan);

            try
            {
                reader.TryGetByte(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Byte: {valueAsStr}");
            }

            try
            {
                reader.TryGetSByte(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as SByte: {valueAsStr}");
            }

            try
            {
                reader.TryGetInt16(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Int16: {valueAsStr}");
            }

            try
            {
                reader.TryGetInt32(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Int32: {valueAsStr}");
            }

            try
            {
                reader.TryGetInt64(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Int64: {valueAsStr}");
            }

            try
            {
                reader.TryGetUInt16(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as UInt16: {valueAsStr}");
            }

            try
            {
                reader.TryGetUInt32(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as UInt32: {valueAsStr}");
            }

            try
            {
                reader.TryGetUInt64(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as UInt64: {valueAsStr}");
            }

            try
            {
                reader.TryGetSingle(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Single: {valueAsStr}");
            }

            try
            {
                reader.TryGetDouble(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Double: {valueAsStr}");
            }

            try
            {
                reader.TryGetDecimal(out _);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Decimal: {valueAsStr}");
            }
        }

        private static void RunTests(byte[] jsonPayload)
        {
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    TestNumberGetMethods(reader);
                    TestNumberTryGetMethods(reader);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Input file: {args[0]}");

            byte[] allBytes = File.ReadAllBytes(args[0]);
            Console.WriteLine($"({allBytes.Length} bytes)");

            // Is BOM present?
            if (allBytes.Length >= 3 && allBytes[0] == 0xEF && allBytes[1] == 0xBB && allBytes[2] == 0xBF)
            {
                Console.WriteLine("BOM present - removing.");
                allBytes = allBytes[3..];
                Console.WriteLine($"({allBytes.Length} bytes remain)");
            }

            RunTests(allBytes);
        }
    }
}
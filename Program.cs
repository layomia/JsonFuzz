using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace utf8fuzz
{
    class Program
    {
        private class JsonFuzzerException : Exception
        {
            public JsonFuzzerException(string message) : base(message) { }
        }

        private static void TestNumberTryGetMethods(byte[] jsonPayload)
        {
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    if (!reader.TryGetByte(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Byte: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetSByte(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as SByte: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetInt16(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Int16: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetInt32(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Int32: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetInt64(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Int64: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetUInt16(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as UInt16: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetUInt32(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as UInt32: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetUInt64(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as UInt64: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetSingle(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Single: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetDouble(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Double: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }

                    if (!reader.TryGetDecimal(out _))
                    {
                        throw new JsonFuzzerException($"Failed to parse as Decimal: {Encoding.UTF8.GetString(reader.ValueSpan)}");
                    }
                }
            }
        }

        private static void TestNumberGetMethods(byte[] jsonPayload)
        {
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    reader.GetByte();
                    reader.GetSByte();
                    reader.GetInt16();
                    reader.GetInt32();
                    reader.GetInt64();
                    reader.GetUInt16();
                    reader.GetUInt32();
                    reader.GetUInt64();
                    reader.GetSingle();
                    reader.GetDouble();
                    reader.GetDecimal();
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

            Console.WriteLine("Fuzzing Number TryGet methods");
            TestNumberTryGetMethods(allBytes);

            Console.WriteLine("Fuzzing Number Get methods");
            TestNumberGetMethods(allBytes);
        }
    }
}
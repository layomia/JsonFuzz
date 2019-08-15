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
                reader.GetByte();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Byte: {valueAsStr}");
            }

            try
            {
                reader.GetSByte();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as SByte: {valueAsStr}");
            }

            try
            {
                reader.GetInt16();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Int16: {valueAsStr}");
            }

            try
            {
                reader.GetInt32();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Int32: {valueAsStr}");
            }

            try
            {
                reader.GetInt64();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Int64: {valueAsStr}");
            }

            try
            {
                reader.GetUInt16();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as UInt16: {valueAsStr}");
            }

            try
            {
                reader.GetUInt32();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as UInt32: {valueAsStr}");
            }

            try
            {
                reader.GetUInt64();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as UInt64: {valueAsStr}");
            }

            try
            {
                reader.GetSingle();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Single: {valueAsStr}");
            }

            try
            {
                reader.GetDouble();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Double: {valueAsStr}");
            }

            try
            {
                reader.GetDecimal();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as Decimal: {valueAsStr}");
            }
        }

        private static void TestStringTryGetMethods(Utf8JsonReader reader)
        {
            Console.WriteLine("Running string TryGet tests.");
            string valueAsStr = Encoding.UTF8.GetString(reader.ValueSpan);

            if (!reader.TryGetDateTime(out _))
            {
                Console.WriteLine($"Failed to parse as DateTime: {valueAsStr}");
            }

            if (!reader.TryGetDateTimeOffset(out _))
            {
                Console.WriteLine($"Failed to parse as DateTimeOffset: {valueAsStr}");
            }

            if (!reader.TryGetGuid(out _))
            {
                Console.WriteLine($"Failed to parse as Guid: {valueAsStr}");
            }
        }

        private static void TestStringGetMethods(Utf8JsonReader reader)
        {
            Console.WriteLine("Running string Get tests.");
            string valueAsStr = Encoding.UTF8.GetString(reader.ValueSpan);

            reader.GetString();

            try
            {
                reader.GetDateTime();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as DateTime: {valueAsStr}");
            }

            try
            {
                reader.GetDateTimeOffset();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as DateTimeOffset: {valueAsStr}");
            }

            try
            {
                reader.GetGuid();
            }
            catch (FormatException)
            {
                Console.WriteLine($"Failed to parse as DateTimeOffset: {valueAsStr}");
            }
        }

        private static void TestCommentGetMethods(Utf8JsonReader reader)
        {
            Console.WriteLine("Running comment Get tests.");
            reader.GetComment();
        }

        private static void TestBooleanGetMethods(Utf8JsonReader reader)
        {
            Console.WriteLine("Running boolean Get tests.");
            reader.GetBoolean();
        }

        private static void TestReaderMethods(byte[] jsonPayload)
        {
            Console.WriteLine("Testing reader methods without using token type.");
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            try
            {
                while (reader.Read())
                {
                    TestCommentGetMethods(reader);
                    TestBooleanGetMethods(reader);
                    TestNumberGetMethods(reader);
                    TestNumberTryGetMethods(reader);
                    TestNumberGetMethods(reader);
                    TestNumberTryGetMethods(reader);
                    TestStringGetMethods(reader);
                    TestStringTryGetMethods(reader);
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void TestReaderMethodsUsingTokenType(byte[] jsonPayload)
        {
            Console.WriteLine("Testing reader methods using token type.");
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.Comment:
                        TestCommentGetMethods(reader);
                        break;
                    case JsonTokenType.False:
                    case JsonTokenType.True:
                        TestBooleanGetMethods(reader);
                        break;
                    case JsonTokenType.Null:
                        TestNumberGetMethods(reader);
                        TestNumberTryGetMethods(reader);
                        break;
                    case JsonTokenType.Number:
                        TestNumberGetMethods(reader);
                        TestNumberTryGetMethods(reader);
                        break;
                    case JsonTokenType.String:
                        TestStringGetMethods(reader);
                        TestStringTryGetMethods(reader);
                        break;
                    case JsonTokenType.PropertyName:
                        reader.GetString();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void TestReadAll(byte[] jsonPayload)
        {
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            Console.WriteLine("Testing read all.");
            try
            {
                while (reader.Read()) { }
            }
            catch(JsonException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void TestSerializer(byte[] jsonPayload)
        {
            try
            {
                object obj = JsonSerializer.Deserialize<object>(jsonPayload);
            }
            catch (ArgumentNullException) { }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RunTests(byte[] jsonPayload)
        {
            TestReadAll(jsonPayload);
            TestReaderMethods(jsonPayload);
            TestReaderMethodsUsingTokenType(jsonPayload);
            TestSerializer(jsonPayload);
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
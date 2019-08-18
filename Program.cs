using System;
using System.IO;
using System.Text.Json;

namespace utf8fuzz
{
    class Program
    {
        private static void TestNumberTryGetMethods(Utf8JsonReader reader)
        {
            reader.TryGetByte(out _);
            reader.TryGetSByte(out _);
            reader.TryGetInt16(out _);
            reader.TryGetInt32(out _);
            reader.TryGetInt64(out _);
            reader.TryGetUInt16(out _);
            reader.TryGetUInt32(out _);
            reader.TryGetUInt64(out _);
            reader.TryGetSingle(out _);
            reader.TryGetDouble(out _);
            reader.TryGetDecimal(out _);
        }

        private static void TestNumberGetMethods(Utf8JsonReader reader)
        {
            try { reader.GetByte(); } catch (FormatException) { }
            try { reader.GetSByte(); } catch (FormatException) { }
            try { reader.GetInt16(); } catch (FormatException) { }
            try { reader.GetInt32(); } catch (FormatException) { }
            try { reader.GetInt64(); } catch (FormatException) { }
            try { reader.GetUInt16(); } catch (FormatException) { }
            try { reader.GetUInt32(); } catch (FormatException) { }
            try { reader.GetUInt64(); } catch (FormatException) { }
            try { reader.GetSingle(); } catch (FormatException) { }
            try { reader.GetDouble(); } catch (FormatException) { }
            try { reader.GetDecimal(); } catch (FormatException) { }
        }

        private static void TestStringTryGetMethods(Utf8JsonReader reader)
        {
            reader.TryGetDateTime(out _);
            reader.TryGetDateTimeOffset(out _);
            reader.TryGetGuid(out _);
        }

        private static void TestStringGetMethods(Utf8JsonReader reader)
        {
            reader.GetString();
            try { reader.GetDateTime(); } catch (FormatException) { }
            try { reader.GetDateTimeOffset(); } catch (FormatException) { }
            try { reader.GetGuid(); } catch (FormatException) { }
        }

        private static void TestCommentGetMethods(Utf8JsonReader reader)
        {
            reader.GetComment();
        }

        private static void TestBooleanGetMethods(Utf8JsonReader reader)
        {
            reader.GetBoolean();
        }

        private static void TestReaderMethods(byte[] jsonPayload)
        {
            JsonReaderOptions options = new JsonReaderOptions()
            {
                CommentHandling = JsonCommentHandling.Allow,
            };
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload, options);

            try
            {
                while (reader.Read())
                {
                    try
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
                    catch (InvalidOperationException) { }
                }
            }
            catch (JsonException) { }
        }

        private static void TestReaderMethodsUsingTokenType(byte[] jsonPayload)
        {
            JsonReaderOptions options = new JsonReaderOptions()
            {
                CommentHandling = JsonCommentHandling.Allow,
            };
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload, options);
            
            try
            {
                while (reader.Read())
                {
                    try
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
                    catch (InvalidOperationException) { }
                }
            }
            catch (JsonException) { }
        }

        private static void TestReadAll(byte[] jsonPayload)
        {
            Utf8JsonReader reader = new Utf8JsonReader(jsonPayload);

            try
            {
                while (reader.Read()) { }
            }
            catch(JsonException) { }
        }

        private static void TestSerializer(byte[] jsonPayload)
        {
            try
            {
                object obj = JsonSerializer.Deserialize<object>(jsonPayload);
            }
            catch (JsonException) { }
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
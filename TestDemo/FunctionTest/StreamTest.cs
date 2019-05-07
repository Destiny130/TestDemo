using System;
using System.Text;
using System.IO;

namespace TestDemo.FunctionTest
{
    public class StreamTest
    {
        public void Start()
        {
            //SimpleExample();

            //TextReaderTest();

            FileStreamTest();
        }

        #region Stream simple example

        void SimpleExample()
        {
            string srcStr = "Stream. Hello world!";
            string dstStr = String.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                print($"Original string is:\t{srcStr}");
                if (ms.CanWrite)
                {
                    //Write source string into memory stream
                    byte[] buffer = Encoding.Default.GetBytes(srcStr);
                    ms.Write(buffer, 0, 3);
                    print($"Stream.Position: {ms.Position}");
                    long newPosition = ms.CanSeek ? ms.Seek(3, SeekOrigin.Current) : 0;
                    print($"After seek, stream.Position: {ms.Position}");
                    if (newPosition < buffer.Length)
                    {
                        ms.Write(buffer, (int)newPosition, buffer.Length - (int)newPosition);
                    }

                    //Write ms into destination string
                    ms.Position = 0;
                    byte[] readBuffer = new byte[ms.Length];
                    int count = ms.CanRead ? ms.Read(readBuffer, 0, (int)ms.Length) : 0;
                    int charCount = Encoding.Default.GetCharCount(readBuffer, 0, count);
                    char[] readCharArr = new char[charCount];
                    Encoding.Default.GetDecoder().GetChars(readBuffer, 0, count, readCharArr, 0);
                    for (int i = 0; i < readCharArr.Length; ++i)
                    {
                        dstStr += readCharArr[i];
                    }
                    print($"Read string is:\t\t{dstStr}");
                }
                ms.Close();
            }
        }

        #endregion Stream simple example

        #region TextReader test

        void TextReaderTest()
        {
            string str = "abc\ndefg";
            using (TextReader reader = new StringReader(str))
            {
                while (reader.Peek() != -1)
                {
                    print($"Peek: {(char)reader.Peek()}");
                    print($"Read: {(char)reader.Read()}");
                }
            }

            using (TextReader reader = new StringReader(str))
            {
                string lineData = reader.ReadLine();
                print($"\nFirst line is: {lineData}");
            }

            using (TextReader reader = new StringReader(str))
            {
                string allData = reader.ReadToEnd();
                print($"\nAll data is: {allData}");
            }
        }

        #endregion TextReader test

        #region FileStream test

        void FileStreamTest()
        {
            string filePath = "../../../Input/Stream Source/TextReader.txt";
            char[] buffer = new char[3];

            using (FileStream fs = File.OpenRead(filePath))
            using (StreamReader reader = new StreamReader(fs))
            {
                DisplayByUsingRead(reader);
            }

            using (FileStream fs = File.OpenRead(filePath))
            using (StreamReader reader = new StreamReader(fs, Encoding.ASCII, false))
            {
                DisplayByUsingReadBlock(reader);
            }

            using (StreamReader reader = new StreamReader(filePath, Encoding.Default, false, 123))
            {
                DisplayByUsingReadLine(reader);
            }

            using (StreamReader reader = File.OpenText(filePath))
            {
                DisplayByUsingReadLine(reader);
            }
        }

        void DisplayByUsingRead(StreamReader reader)
        {
            int readChar = 0;
            StringBuilder sb = new StringBuilder();
            while ((readChar = reader.Read()) != -1)
            {
                sb.Append((char)readChar);
            }
            print($"Using StreamReader.Read() get text content: {sb.ToString()}");
        }

        void DisplayByUsingReadBlock(StreamReader reader)
        {
            char[] buffer = new char[10];
            StringBuilder sb = new StringBuilder();
            reader.ReadBlock(buffer, 0, 10);
            for (int i = 0; i < buffer.Length; ++i)
            {
                sb.Append(buffer[i]);
            }
            print($"Using StreamReader.ReadBlock() get text first 10 content: {sb.ToString()}");
        }

        void DisplayByUsingReadLine(StreamReader reader)
        {
            int i = 1;
            string result = String.Empty;
            while ((result = reader.ReadLine()) != null)
            {
                print($"Using StreamReader.ReadLine() get text {ordinalFunc(i)} line content: {result}");
                ++i;
            }
        }

        #endregion FileStream test

        #region Delegates

        Action<string> print = str => Console.WriteLine(str);

        Func<int, string> ordinalFunc = i => i % 10 == 1 ? $"{i}st" : (i % 10 == 2 ? $"{i}nd" : (i % 10 == 3 ? $"{i}rd" : $"{i}th"));

        Func<int, string> ordinalFunc2 = i =>
        {
            int mod = i % 10;
            string result = i.ToString();
            switch (mod)
            {
                case 1:
                    result += "st";
                    break;
                case 2:
                    result += "nd";
                    break;
                case 3:
                    result += "rd";
                    break;
                default:
                    result += "th";
                    break;
            }
            return result;
        };

        #endregion Delegates
    }
}

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

            TextReaderTest();
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

        #region Delegates

        Action<string> print = str => Console.WriteLine(str);

        #endregion Delegates
    }
}

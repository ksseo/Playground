
using System;
using System.Linq;

namespace ByteWriter
{

    class Program
    {
        public const int SoundOpenRequestMessageLength = 7;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ByteWriter byteWriter = new ByteWriter(SoundOpenRequestMessageLength);

            byte[] message = byteWriter.Write((byte)0x03).Write((byte)0x00).EndWrite();

            Array.Resize(ref message, SoundOpenRequestMessageLength);

            Console.WriteLine(message.Length);

        }
    }
}

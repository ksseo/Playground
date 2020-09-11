using Lucene.Net.Analysis.Payloads;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

// ksseo: 2020-08-11
// https://programmer.help/blogs/a-fast-method-of-writing-various-data-into-byte-array-in-c.html
namespace ByteWriter
{
    class ByteWriter
    {
        byte[] buffer;
        int i;

        public ByteWriter(int length)
        {
            buffer = ArrayPool<byte>.Shared.Rent(length);
            i = 0;
        }

        public ByteWriter Write(byte v)
        {
            buffer[i++] = v;
            return this;
        }

        public void Write(int v)
        {
            buffer[i++] = (byte)v;
            buffer[i++] = (byte)(v >> 8);
            buffer[i++] = (byte)(v >> 16);
            buffer[i++] = (byte)(v >> 24);
        }

        public void Write(long v)
        {
            buffer[i++] = (byte)v;
            buffer[i++] = (byte)(v >> 8);
            buffer[i++] = (byte)(v >> 16);
            buffer[i++] = (byte)(v >> 24);
            buffer[i++] = (byte)(v >> 32);
            buffer[i++] = (byte)(v >> 40);
            buffer[i++] = (byte)(v >> 48);
            buffer[i++] = (byte)(v >> 56);
        }

        public void Write(ulong v)
        {
            buffer[i++] = (byte)v;
            buffer[i++] = (byte)(v >> 8);
            buffer[i++] = (byte)(v >> 16);
            buffer[i++] = (byte)(v >> 24);
            buffer[i++] = (byte)(v >> 32);
            buffer[i++] = (byte)(v >> 40);
            buffer[i++] = (byte)(v >> 48);
            buffer[i++] = (byte)(v >> 56);
        }

        // Cannot find package below: Float64bits 
        //public void Write(double v)
        //{
        //    Write(FloatEncoder.Float64bits(v));
        //}

        public void Write(string v)
        {
            byte[] strBytes = System.Text.Encoding.Default.GetBytes(v);
            int len = strBytes.Length;
            Write(len);
            for (int j = 0; j < len; j++)
            {
                buffer[i++] = strBytes[j];
            }
        }

        public byte[] EndWrite()
        {
            ArrayPool<byte>.Shared.Return(buffer);
            return buffer;
        }
    }
}

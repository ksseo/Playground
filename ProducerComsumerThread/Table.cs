using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ProducerComsumerDataflow
{
    class Table
    {
        private readonly String[] buffer;
        private int tail;
        private int head;
        private int count;

        public Table(int count)
        {
            this.buffer = new string[count];
            this.tail = 0;
            this.head = 0;
            this.count = 0;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void put(String cake) {
            Console.WriteLine(Thread.CurrentThread.Name + " puts " + cake);


            while (count >= buffer.Length)
            {
                Monitor.Wait(this);
            }
            buffer[tail] = cake;
            tail = (tail + 1) % buffer.Length;
            count++;
            Monitor.PulseAll(this);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public String take()
        {
            while (count <= 0)
            {
                Monitor.Wait(this);
            }

            String cake = buffer[head];
            head = (head + 1) % buffer.Length;
            count--;
            Monitor.PulseAll(this);

            Console.WriteLine(Thread.CurrentThread.Name + " takes " + cake);
            return cake;
        }
    }
}

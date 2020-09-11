using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Transactions;

namespace ProducerComsumerDataflow
{
    class MakerThread
    {
        private readonly Random random;
        private readonly Table table;
        private static int id = 0;
        private String name;
        public Thread maker;
        public static Task producer;

        public MakerThread(String name, Table table, int seed)
        {
            this.name = name;
            this.random = new Random(seed);
            this.table = table;
        }

        public void run()
        {
            maker = new Thread(new ThreadStart(async delegate {
                try
                {
                    while (true)
                    {
                        String cake = "[ Cake No." + nextId() + " by " + Thread.CurrentThread.Name + " ]";

                        Table.buffer = new BufferBlock<String>(new DataflowBlockOptions { BoundedCapacity = 1024, });
                        producer =  Table.put(Table.buffer, cake);
                        //EaterThread.consumer = Table.take(Table.buffer);

                        //await Task.WhenAll(producer, EaterThread.consumer, Table.buffer.Completion);

                        //Thread.Sleep(random.Next(1000));
                    }

                    Console.WriteLine("MakerThread");
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine("MakerThread ThreadInterruptedException");
                }
            }));
            maker.Name = name;
            maker.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static int nextId()
        {
            return id++;
        }
    }
}

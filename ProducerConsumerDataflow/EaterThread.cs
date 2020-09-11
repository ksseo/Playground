using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProducerComsumerDataflow
{
    class EaterThread
    {
        private readonly Random random;
        private readonly Table table;
        private String name;
        public Thread eater;
        public static Task<String> consumer;

        public EaterThread(String name, Table table, int seed)
        {
            this.name = name;
            this.random = new Random(seed);
            this.table = table;
        }

        public void run()
        {
            eater = new Thread(new ThreadStart(async delegate {
                try
                {
                    //Thread.Sleep(1000);

                    while (true)
                    {
                        //String cake = table.take(Table.buffer);

                        //if (Table.buffer != null)
                        //{
                        //    var consumer = table.take(Table.buffer);
                        //    consumer.Wait();
                        //    //Thread.Sleep(random.Next(1000));
                        //}

                        if (Table.buffer != null)
                        {
                            if (MakerThread.producer != null)
                            {
                                consumer = Table.take(Table.buffer);
                                await Task.WhenAll(MakerThread.producer, consumer, Table.buffer.Completion);
                                var result = await consumer;
                            }

                            //Thread.Sleep(random.Next(1000));
                        }

                        //if (consumer != null)
                        //{
                        //    var result = await consumer;
                        //}
                    }
                    Console.WriteLine("EaterThread");

                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine("EaterThread ThreadInterruptedException");
                }
            }));
            eater.Name = name;
            eater.Start();
        }
    }
}

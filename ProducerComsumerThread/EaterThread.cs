using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProducerComsumerDataflow
{
    class EaterThread
    {
        private readonly Random random;
        private readonly Table table;
        private String name;
        public Thread eater;

        public EaterThread(String name, Table table, int seed)
        {
            this.name = name;
            this.random = new Random(seed);
            this.table = table;
        }

        public void run()
        {
            eater = new Thread(new ThreadStart( delegate {
                try
                {
                    while (true)
                    {
                        String cake = table.take();
                        Thread.Sleep(random.Next(1000));
                    }
                }
                catch (ThreadInterruptedException e)
                {

                }
            }));
            eater.Name = name;
            eater.Start();
        }
    }
}

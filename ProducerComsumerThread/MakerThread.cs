using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
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

        public MakerThread(String name, Table table, int seed)
        {
            this.name = name;
            this.random = new Random(seed);
            this.table = table;
        }

        public void run()
        {
            maker = new Thread(new ThreadStart(delegate {
                try
                {
                    while (true)
                    {
                        Thread.Sleep(random.Next(1000));
                        String cake = "[ Cake No." + nextId() + " by " + Thread.CurrentThread.Name + " ]";
                        table.put(cake);
                    }
                }
                catch (ThreadInterruptedException e)
                {

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

using System;

namespace ProducerComsumerDataflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(3);

            MakerThread makerThread1 = new MakerThread("MakerThread-1", table, 31415);
            //MakerThread makerThread2 = new MakerThread("MakerThread-2", table, 69583);
            //MakerThread makerThread3 = new MakerThread("MakerThread-3", table, 85246);

            EaterThread eaterThread1 = new EaterThread("EaterThread-1", table, 75865);
            //EaterThread eaterThread2 = new EaterThread("EaterThread-2", table, 66339);
            //EaterThread eaterThread3 = new EaterThread("EaterThread-3", table, 89654);

            makerThread1.run();
            //makerThread2.run();
            //makerThread3.run();

            eaterThread1.run();
            //eaterThread2.run();
            //eaterThread3.run();
        }
    }
}

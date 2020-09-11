using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProducerComsumerDataflow
{
    public class Table
    {
        public static BufferBlock<String> buffer = null;

        public Table()
        {

        }

        // https://blog.stephencleary.com/2012/11/async-producerconsumer-queue-using.html

        public static async Task put(ITargetBlock<String> target, String cake) {
            Console.WriteLine(Thread.CurrentThread.Name + " puts " + cake);

            //target.Post(cake);
            await target.SendAsync(cake);
            target.Complete();
        }
                
        public static async Task<String> take(ISourceBlock<String> source)
        {
            String cake = null;

            while (await source.OutputAvailableAsync())
            {
                //cake = source.Receive();
                cake = await source.ReceiveAsync();
                Console.WriteLine(Thread.CurrentThread.Name + " takes " + cake);
            }

            //Console.WriteLine(Thread.CurrentThread.Name + " takes " + cake);
            return cake;
        }
    }
}

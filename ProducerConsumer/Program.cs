using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

// Demonstrates a basic producer and consumer pattern that uses dataflow.
class DataflowProducerConsumer
{
    //private BufferBlock<byte[]> buffer;
    private static BufferBlock<byte[]> buffer;

    // Demonstrates the production end of the producer and consumer pattern.
    static void Produce(ITargetBlock<byte[]> target, byte[] data)
    {
        // Create a Random object to generate random data.
        Random rand = new Random();

        // In a loop, fill a buffer with random data and
        // post the buffer to the target block.
        for (int i = 0; i < 100; i++)
        {
            // Create an array to hold random byte data.
            byte[] buffer = new byte[1024];

            // Fill the buffer with random bytes.
            rand.NextBytes(buffer);

            // Post the result to the message block.
            target.Post(buffer);
        }

        // Set the target to the completed state to signal to the consumer
        // that no more data will be available.
        target.Complete();
    }

    // Demonstrates the consumption end of the producer and consumer pattern.
    static async Task<int> ConsumeAsync(ISourceBlock<byte[]> source)
    {
        // Initialize a counter to track the number of bytes that are processed.
        int bytesProcessed = 0;

        // Read from the source buffer until the source buffer has no
        // available output data.
        while (await source.OutputAvailableAsync())
        {
            byte[] data = source.Receive();

            // Increment the count of bytes received.
            bytesProcessed += data.Length;
        }

        return bytesProcessed;
    }

    static void Main(string[] args)
    {

        while (true) {
            // Create a BufferBlock<byte[]> object. This object serves as the
            // target block for the producer and the source block for the consumer.
            //var buffer = new BufferBlock<byte[]>();

            buffer = new BufferBlock<byte[]>();

            // Start the consumer. The Consume method runs asynchronously.
            var consumer = ConsumeAsync(buffer);

            // Wait for the consumer to process all data.
            consumer.Wait();

            byte[] data = new byte[100];

            // Post source data to the dataflow block.
            Produce(buffer, data);



            // Print the count of bytes processed to the console.
            Console.WriteLine("Processed {0} bytes.", consumer.Result);
        }
    }
}

/* Output:
Processed 102400 bytes.
*/
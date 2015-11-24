using System;

namespace Blog.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Необходимо запускать с параметрами 'server' 'exchange name' 'routing'");
                Console.ReadLine();
                return;
            }

            string serverName = args[0];
            string exchangeName = args[1];
            string routingName = args[2];

        }
    }
}

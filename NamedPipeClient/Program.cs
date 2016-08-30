using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace NamedPipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipeFactory = new ChannelFactory<IMessageDisplayer>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Reverse"));
            var pipeProxy = pipeFactory.CreateChannel();

            Console.WriteLine("Client started. Type ahead. Enter blank line to exit.");

            while (true)
            {
                var input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.Enter) break;

                pipeProxy.DisplayCharacter(input.KeyChar);
            }
        }

        [ServiceContract]
        public interface IMessageDisplayer
        {
            [OperationContract]
            void DisplayMessage(string value);

            [OperationContract]
            void DisplayCharacter(char value);
        }
    }
}

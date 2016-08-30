using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var serviceHost = new ServiceHost(typeof(MessageDisplayer), new Uri("net.pipe://localhost")))
            {
                serviceHost.AddServiceEndpoint(typeof(IMessageDisplayer), new NetNamedPipeBinding(), "Reverse");
                serviceHost.Open();

                Console.WriteLine("Service is available. Press any key to exit.");
                Console.ReadKey();

                serviceHost.Close();
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

        public class MessageDisplayer : IMessageDisplayer
        {
            public void DisplayMessage(string value)
            {
                Console.WriteLine(value);
            }

            public void DisplayCharacter(char value)
            {
                Console.Write(value);
            }
        }
    }
}

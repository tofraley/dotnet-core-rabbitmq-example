using System;
using DirectPaymentCardConsumer.RabbitMQ;

namespace DirectPaymentCardConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQConsumer client = new RabbitMQConsumer();
            client.CreateConnection();
            client.ProcessMessages();  
        }
    }
}

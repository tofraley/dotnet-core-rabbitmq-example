using System;
using PaymentCardConsumer.RabbitMQ;

namespace PaymentCardConsumer
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

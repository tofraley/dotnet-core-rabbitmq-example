using System;
using PurchaseOrderConsumer.RabbitMQ;

namespace PurchaseOrderConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQConsumer client = new RabbitMQConsumer();
            client.CreateConnection();
            client.ProcessMessages();
            client.Close();
        }
    }
}

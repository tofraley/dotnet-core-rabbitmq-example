using System;
using AccountsAuditConsumer.RabbitMQ;

namespace AccountsAuditConsumer
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

using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace AccountsAuditConsumer.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;        

        private const string ExchangeName = "Topic_Exchange";
        private const string AllQueueName = "AllTopic_Queue";

        public void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };            
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ProcessMessages()
        {
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    Console.WriteLine("Listening for Topic <payment.*>");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine();
                    
                    channel.ExchangeDeclare(ExchangeName, "topic");
                    channel.QueueDeclare(AllQueueName, true, false, false, null);
                    channel.QueueBind(AllQueueName, ExchangeName, "payment.*");

                    channel.BasicQos(0, 10, false);
                    Subscription subscription = new Subscription(channel, AllQueueName, false);                    

                    while (true)
                    {
                        BasicDeliverEventArgs deliveryArguments = subscription.Next();
                        
                        var message = deliveryArguments.Body.DeSerializeText();

                        Console.WriteLine("Message Received '{0}'", message);
                        subscription.Ack(deliveryArguments);
                    }
                }
            }
        }
    }
}
using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace PurchaseOrderConsumer.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;        

        private const string ExchangeName = "Topic_Exchange";
        private const string PurchaseOrderQueueName = "PurchaseOrderTopic_Queue";

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
                    Console.WriteLine("Listening for Topic <payment.purchaseorder>");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine();
                    
                    channel.ExchangeDeclare(ExchangeName, "topic");
                    channel.QueueDeclare(PurchaseOrderQueueName, true, false, false, null);
                    channel.QueueBind(PurchaseOrderQueueName, ExchangeName, "payment.purchaseorder");

                    channel.BasicQos(0, 10, false);
                    Subscription subscription = new Subscription(channel, PurchaseOrderQueueName, false);
                    
                    while (true)
                    {
                        BasicDeliverEventArgs deliveryArguments = subscription.Next();

                        var message = (PurchaseOrder)deliveryArguments.Body.DeSerialize(typeof(PurchaseOrder));
                        var routingKey = deliveryArguments.RoutingKey;

                        Console.WriteLine("-- Purchase Order - Routing Key <{0}> : {1}, £{2}, {3}, {4}", routingKey, message.CompanyName, message.AmountToPay, message.PaymentDayTerms, message.PoNumber);
                        subscription.Ack(deliveryArguments);
                    }
                }
            }
        }
    }
}

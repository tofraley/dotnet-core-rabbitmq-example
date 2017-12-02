using System;
using System.Collections.Generic;
using rabbitmq_example_api.Models;
using RabbitMQ.Client;

namespace rabbitmq_example_api.RabbitMQ
{
    public class RabbitMQClient
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "Topic_Exchange";
        private const string CardPaymentQueueName = "CardPaymentTopic_Queue";
        private const string PurchaseOrderQueueName = "PurchaseOrderTopic_Queue";
        private const string AllQueueName = "AllTopic_Queue";

        public RabbitMQClient()
        {
            CreateConnection();
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost", UserName = "guest", Password = "guest"
            };

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "topic");

            _model.QueueDeclare(CardPaymentQueueName, true, false, false, null);
            _model.QueueDeclare(PurchaseOrderQueueName, true, false, false, null);
            _model.QueueDeclare(AllQueueName, true, false, false, null);

            _model.QueueBind(CardPaymentQueueName, ExchangeName, "payment.cardpayment");
            _model.QueueBind(PurchaseOrderQueueName, ExchangeName, 
                "payment.purchaseorder");

            _model.QueueBind(AllQueueName, ExchangeName, "payment.*");
        }

        public void Close()
        {
            _connection.Close();
        }

        public void SendPayment(CardPayment payment)
        {
            SendMessage(payment.Serialize(), "payment.cardpayment");
            Console.WriteLine(" Payment Sent {0}, £{1}", payment.CardNumber, 
                payment.Amount);
        }

        public void SendPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            SendMessage(purchaseOrder.Serialize(), "payment.purchaseorder");

            Console.WriteLine(" Purchase Order Sent {0}, £{1}, {2}, {3}", 
                purchaseOrder.CompanyName, purchaseOrder.AmountToPay, 
                purchaseOrder.PaymentDayTerms, purchaseOrder.PoNumber);
        }

        public void SendMessage(byte[] message, string routingKey)
        {
            _model.BasicPublish(ExchangeName, routingKey, null, message);
        }
    }  
}

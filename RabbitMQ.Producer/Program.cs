using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Producer
{
    static class Program
    {
        static void Main(string[] args)
        {
            //RabbitMQ.Client used

            //Connection Factory
            var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") }; 
            //amqp://username:password:host:port
            using var connection = factory.CreateConnection(); //return iconnection object
            using var channel = connection.CreateModel(); // return imodel nothing but channel
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false ,autoDelete:false,arguments:null);
            // durable true - msg hang around until user reads it

            var message = new { Name = "Producer", Message = "Hello! " };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            // data pass in bytes

            channel.BasicPublish("", "demo-queue", null, body);


        }
    }
}

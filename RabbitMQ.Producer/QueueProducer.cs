using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            // durable true - msg hang around until user reads it

            var count = 0;
            //for producing n message use loop
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count : {count }" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                // data pass in bytes

                channel.BasicPublish("", "demo-queue", null, body);// default amqp exchange
                count++;
                Thread.Sleep(1000);
            }
   

        }
    }
}

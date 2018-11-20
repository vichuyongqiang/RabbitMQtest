using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQTestReceive
{
    class Program
    {
        public static ConnectionFactory factory = new ConnectionFactory();
        static void Main(string[] args)
        {

            factory.HostName = "localhost";
            factory.UserName = "vic";
            factory.Password = "123zxcvb";
            //不同的Queue，开不同线程去监听接收
            Thread t = new Thread(new ThreadStart(helloqueue));
            t.Start();
            Thread t2 = new Thread(new ThreadStart(vicqueue));
            t2.Start();
        }

        public static void helloqueue()
        {
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);
                    channel.BasicQos(0, 1, false);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("hello", true, consumer);
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("hello已接收： {0}", message);

                    }

                }
            }
        }


        public static void vicqueue()
        {
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("vic_name", false, false, false, null);
                    channel.BasicQos(0, 1, false);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("vic_name", true, consumer);
                    while (true) {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("vic_name已接收： {0}", message);
                    }
                }
            }
        }
    }
}

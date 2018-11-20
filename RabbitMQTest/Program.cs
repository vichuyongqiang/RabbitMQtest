using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "vic";//用户名
            factory.Password = "123zxcvb";//密码

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);//创建一个名称为hello的消息队列
                    string message = "Hello World"; //传递的消息内容
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "hello", null, body); //开始传递
                    Console.WriteLine("已发送： {0}", message);
                    Console.ReadLine();
                }
            }
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("vic_name", false, false, false, null);//创建一个名称为hello的消息队列
                    string message = "huyongqiang"; //传递的消息内容
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "vic_name", null, body); //开始传递
                    Console.WriteLine("已发送： {0}", message);
                    Console.ReadLine();
                }
            }
        }
    }
}

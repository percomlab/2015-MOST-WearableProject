using UnityEngine;
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;


namespace AssemblyCSharp
{
	public class RabbitMQServerThread
	{
	  ConnectionFactory factory;
		public static string MESSAGE;

	  public RabbitMQServerThread ()
      {
		
	  }
	  
      public void Run(){

			Debug.Log("Server");
			factory = new ConnectionFactory() { Uri = "amqp://admin:admin@192.168.4.109" };
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.ExchangeDeclare("COMMAND", "fanout");
					
					var queueName = channel.QueueDeclare().QueueName;
					
					channel.QueueBind(queueName, "COMMAND", "");
					var consumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume(queueName, true, consumer);

					Debug.Log(" [*] Waiting for logs. To exit press CTRL+C");


					while (true) {
						 var ea = (BasicDeliverEventArgs) consumer.Queue.Dequeue ();
						 var body = ea.Body;
						 var message = Encoding.UTF8.GetString (body);
						 Debug.Log (message);
						 if(message.Equals("Hello")){
							  channel.BasicPublish("COMMAND", "", null, Encoding.UTF8.GetBytes("world!"));
							  MESSAGE = message;
						 }
					}

				}
			}
	  }
	 
	}
}

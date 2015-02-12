using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using AssemblyCSharp;

public class NewBehaviourScript : MonoBehaviour {

	public static Boolean buttonClick = false;
	GUIStyle test=new GUIStyle();
	ConnectionFactory factory;
	IConnection connection;
	IModel channel;
	public static string stringToEdit = "";

	// Use this for initialization
	void Start () {
		factory = new ConnectionFactory() { Uri = "amqp://admin:admin@192.168.4.109" };
		connection = factory.CreateConnection ();
		channel = connection.CreateModel ();
		channel.ExchangeDeclare ("COMMAND", "fanout");
		
		var queueName = channel.QueueDeclare ().QueueName;
		
		channel.QueueBind (queueName, "COMMAND", "");
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void OnGUI(){
		var receiveMes = AssemblyCSharp.RabbitMQServerThread.MESSAGE;

		GUI.color = Color.white;
		test.fontSize = 15;
		stringToEdit = GUI.TextField(new Rect(100, 150, 180, 30), stringToEdit, 25);
		GUI.color = new Color(0,1,0,1);
		if (GUI.Button (new Rect (300,150, 100, 30), "send message")) {

					channel.BasicPublish("COMMAND", "", null, Encoding.UTF8.GetBytes("" + stringToEdit));
			}
				
		GUI.color = new Color(1,0,1,1);

		GUI.Button (new Rect (120, 200, 220, 70), "receiveMessage :       " + receiveMes, test);
	}

}


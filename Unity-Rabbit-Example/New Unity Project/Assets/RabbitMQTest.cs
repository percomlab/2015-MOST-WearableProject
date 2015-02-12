using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using AssemblyCSharp;

public class RabbitMQTest : MonoBehaviour {

	RabbitMQServerThread server;

	Thread t;

	// Use this for initialization
	void Start () {

				server = new RabbitMQServerThread();
				t = new Thread (server.Run);
				t.Start ();
				Debug.Log(" [*] Waiting for logs. To exit press CTRL+C");
	}
	
	// Update is called once per frame
	void Update () {

	}

}

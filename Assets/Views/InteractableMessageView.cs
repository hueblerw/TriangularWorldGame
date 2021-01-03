using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMessageView
{
    
	public GameObject messageFrame;

	private string message;
	private Dictionary<string, string> buttonOptions;

	public InteractableMessageView(string message, Dictionary<string, string> buttonOptions)
	{
		messageFrame = new GameObject("messageFrame");
		this.message = message;
		this.buttonOptions = buttonOptions;
	}

	public string getResponseInfo()
	{
		return "???";
	}

}

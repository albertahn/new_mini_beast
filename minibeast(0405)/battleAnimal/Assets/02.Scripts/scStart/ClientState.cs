using UnityEngine;
using System.Collections;

public class ClientState : MonoBehaviour {
	public static string id;
	public static bool isMaster;
	public static string room;
	public static int order;
	// Use this for initialization

	void Awake(){
		id = PlayerPrefs.GetString ("email");
		isMaster = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.Label(new Rect(200,10,50,50),"id = "+id);
	}
}

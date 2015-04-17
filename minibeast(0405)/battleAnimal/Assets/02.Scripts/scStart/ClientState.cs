using UnityEngine;
using System.Collections;

public class ClientState : MonoBehaviour {
	public static string id;
	public static bool isMaster;
	public static string room;
	public static int order;
	public static string character;
	public static string team;
	// Use this for initialization

	void Awake(){
		id = PlayerPrefs.GetString ("email");
		isMaster = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		/*GUI.Label(new Rect(200,10,50,50),"id = "+id);
		GUI.Label(new Rect(200,70,50,50),"room = "+room);
		GUI.Label(new Rect(200,130,50,50),"order = "+order);
		GUI.Label(new Rect(200,190,50,50),"character = "+character);
		GUI.Label(new Rect(200,250,50,50),"team = "+team);*/
	}
}

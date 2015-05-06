using UnityEngine;
using System.Collections;

public class ClientState : MonoBehaviour {
	public static string id;
	public static bool isMaster;
	public static string room;
	public static int order;
	public static string character;
	public static string team;
	public static int kill,death,level;
	public static int exp;
	public static int money;
	public static string[] inventory;
	
	public static int idx;
	// Use this for initialization
	
	void Awake(){
		id = PlayerPrefs.GetString ("email");
		isMaster = false;
		exp = 0;
		level = 1;
		money = 0;
		idx = 0;
		
		inventory = new string[3];
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public static void addInventory(string a){
		inventory [idx] = a.ToString();
		idx ++;
	}
	
	void OnGUI(){
		GUI.Label(new Rect(200,10,50,50),"id = "+id);
		GUI.Label(new Rect(200,70,50,50),"room = "+room);
		GUI.Label(new Rect(200,130,50,50),"order = "+order);
		GUI.Label(new Rect(200,190,50,50),"character = "+character);
		GUI.Label(new Rect(200,250,50,50),"team = "+team);
	}
}
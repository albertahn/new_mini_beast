using UnityEngine;
using System.Collections;

public class waitGUI : MonoBehaviour {
	public Texture2D dogPortrait,turtlePortrait,randomPortrait,
	emptyPortrait,nameTag;

	private string[] name;
	public Texture2D[] portrait;

	private int userNum;

	// Use this for initialization
	void Start (){
		userNum = 0;
		name = new string[6];
		portrait = new Texture2D[6];

		for(int i=0;i<6;i++)
			name [i] = "";

		for(int i=0;i<6;i++)
			portrait[i] = emptyPortrait;

		switch(Random.Range(1,3)){
		case 1:
			ClientState.character = "dog";					
			break;
		case 2:
			ClientState.character = "turtle";						
			break;
		}
	}
	/*
	id = ClientID;
	character = ClientState.character;
	*/
	public void addUser(int _order,string _id){
		name [_order] = _id;
		portrait [_order] = randomPortrait;
		userNum++;
	}

	public void deleteUser(int _order){
		Debug.Log ("order = " + _order);
		name [_order] = "";
		portrait [_order] = emptyPortrait;
		userNum--;
	}
	
	// Update is called once per framess
	void Update () {

	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(20,50,100,100),portrait[0]);
		GUI.DrawTexture (new Rect(20,160,100,40),nameTag);
		GUI.Label (new Rect(40,170,100,40),name[0]);
		GUI.DrawTexture (new Rect(160,50,100,100),portrait[1]);
		GUI.DrawTexture (new Rect(160,160,100,40),nameTag);
		GUI.Label (new Rect(180,170,100,40),name[1]);
		GUI.DrawTexture (new Rect(300,50,100,100),portrait[2]);
		GUI.DrawTexture (new Rect(300,160,100,40),nameTag);
		GUI.Label (new Rect(320,170,100,40),name[2]);

				
		GUI.DrawTexture (new Rect(20,220,100,100),portrait[3]);
		GUI.DrawTexture (new Rect(20,330,100,40),nameTag);
		GUI.Label (new Rect(40,340,100,40),name[3]);
		GUI.DrawTexture (new Rect(160,220,100,100),portrait[4]);
		GUI.DrawTexture (new Rect(160,330,100,40),nameTag);
		GUI.Label (new Rect(180,340,100,40),name[4]);
		GUI.DrawTexture (new Rect(300,220,100,100),portrait[5]);
		GUI.DrawTexture (new Rect(300,330,100,40),nameTag);
		GUI.Label (new Rect(320,340,100,40),name[5]);

		if (GUI.Button (new Rect(10,400,100,50),"dog")) {
			//portrait[ClientState.order] = dogPortrait;
			string data = ClientState.id +":"+ClientState.order+":dog";
			waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
			ClientState.character = "dog";
		}

		if (GUI.Button (new Rect(120,400,100,50),"turtle")) {
			//portrait[ClientState.order] = turtlePortrait;
			string data = ClientState.id +":"+ClientState.order+":turtle";
			waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
			ClientState.character = "turtle";			
		}

		if (GUI.Button (new Rect(230,400,100,50),"random")) {
			//portrait[ClientState.order] = turtlePortrait;
			string data = ClientState.id +":"+ClientState.order+":random";
			waitSocketStarter.Socket.Emit ("characterSelectREQ", data);
			switch(Random.Range(1,3)){
			case 1:
				ClientState.character = "dog";					
				break;
			case 2:
				ClientState.character = "turtle";						
				break;
			}
		}

		if (GUI.Button (new Rect (200, 500, 100, 50), "Ready")) {
			if(0<=ClientState.order && ClientState.order<=2){
				ClientState.team = "red";
			}else
				ClientState.team = "blue";
			Application.LoadLevel("scMulty");
		}

		if(GUI.Button (new Rect(200+(100+10),500,100 ,50),"Back")){
			Application.LoadLevel("scStart");
		}


		GUI.Label(new Rect(200,10,50,50),"id = "+ClientState.id);
		GUI.Label(new Rect(200,70,50,50),"room = "+ClientState.room);
		GUI.Label(new Rect(200,130,50,50),"order = "+ClientState.order);
		GUI.Label(new Rect(200,190,50,50),"character = "+ClientState.character);
		GUI.Label(new Rect(200,250,50,50),"team = "+ClientState.team);
	}

	public void setCharacter(int _order,string _char){
		if (_char == "dog")
			portrait [_order] = dogPortrait;
		else if(_char =="turtle")
			portrait [_order] = turtlePortrait;
		else if(_char =="random")
			portrait [_order] = randomPortrait;
	}
}
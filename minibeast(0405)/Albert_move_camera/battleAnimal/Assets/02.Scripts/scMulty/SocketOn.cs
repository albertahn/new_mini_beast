using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//An element with the same key already exists in the dictionary에러를 피하려면 Socket.on을 모아놔야 한다.
public class SocketOn : MonoBehaviour {
	private SpawnPlayer _spawnPlayer;
	private MoveCtrl _moveCtrl;
	private NetworkState _networkState;

	private string ClientID;
	private string[] users_name;

	private List<GameObject> users; 

	private bool addUserSwitch;
	private string addId;
	
	public string resID;
	public Vector3 newPos;
	private bool moveUserSwitch;

	// Use this for initialization
	void Start () {
		addUserSwitch = false;
		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_networkState = GetComponent<NetworkState> ();
		ClientID = ClientState.id;
		
		moveUserSwitch=false;

		users = new List<GameObject> ();

		SocketStarter.Socket.On("createPlayerRES",(data) =>
		                        {//접속한 플레이어가 있을때 호출된다.
			string temp = data.Json.args[0].ToString();
			string[] pos;
			Vector3 spawnPos;
			
			pos = temp.Split(':');
			addId = pos[0];//접속한 유저의 아이디
			pos = pos[1].Split(',');
			
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));
			
			_networkState.addPlayer(addId,spawnPos);	//플레이어의 아이디와 위치 저장
			users_name = _networkState.getPlayerList();	//users 아이디의 배열을 얻어온다.
			
			_spawnPlayer.setSpawn(addId,spawnPos);//해당 user를 instantiate한다.
			
			addUserSwitch = true;//하이어아키에 만들어진 유저오브젝트를 찾아서 추가한다.
			
			for(int i=0;i<users_name.Length;i++){

				Debug.Log ("list: "+users_name[i]);

			}
		});

		SocketStarter.Socket.On ("movePlayerRES", (data) =>
		                         {

			string[] temp = data.Json.args[0].ToString().Split(':');
			resID = temp[0];
			string[] resPos = temp[1].Split(',');
			newPos = new Vector3(float.Parse(resPos[0]),
			                     float.Parse(resPos[1]),
			                     float.Parse(resPos[2]));
			moveUserSwitch=true;
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (addUserSwitch) {

			users.Add (GameObject.Find(addId));
			addUserSwitch=false;
		}

		if (moveUserSwitch) {
			moveUser();
			moveUserSwitch=false;
		}	
	}

	void moveUser(){
		foreach (GameObject a in users) {
			if(a.name==resID&&a.name!=ClientID){
				a.transform.position = Vector3.Lerp (a.transform.position, newPos, Time.deltaTime * 10.0f);
			}
		}
	}
}
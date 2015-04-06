using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	public GameObject player;
	bool spawnSwitch;//socket.on에서 instantiate를 하면 에러가 나서 업데이트 문에서 쓰기위한 꼼수
	Vector3 spawnPos;
	private string ClientID;//접속한 사람의 id	

	IEnumerator CreatePlayer(){
		spawnSwitch = false;
		//Vector3 pos = new Vector3 (430.0f,0.06f,226.0f);

		string data = PlayerPrefs.GetString("email")+":25.0,50,25";//접속한 유저의 아이디와 생성할 위치를 서버에 전송
		SocketStarter.Socket.Emit("createPlayerREQ",data);
		yield return null;
	}
	
	private string id; //접속한 유저의 아이디
	// Use this for initialization
	void Start () {

		StartCoroutine (CreatePlayer());
		spawnSwitch = false;
		ClientID = ClientState.id;

	}

	public void setSpawn(string _id,Vector3 pos){
		id = _id;
		spawnPos = pos;
		spawnSwitch=true;
	}

	// Update is called once per frame
	void Update () {
		if (spawnSwitch) {

			GameObject a;
			a = (GameObject)Instantiate(player,spawnPos,Quaternion.identity);
			a.name=id;
			spawnSwitch = false;

			if(id!=ClientID){//접속한 유저가 내가 아닐때
				//a.GetComponent<MoveCtrl>().enabled = false;
			}
			else{//접속한 유저가 나일때
				//Camera.main.GetComponent<FollowCam>().setTarget(a.GetComponent<Transform>());
			}
		}
	}
}
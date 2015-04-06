using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	public GameObject player;
	public bool spawnSwitch;//socket.on에서 instantiate를 하면 에러가 나서 업데이트 문에서 쓰기위한 꼼수
	Vector3 spawnPos;
	private string ClientID;//접속한 사람의 id	

	public IEnumerator CreatePlayer(){
		spawnSwitch = false;
		//Vector3 pos = new Vector3 (430.0f,0.06f,226.0f);

		string data = PlayerPrefs.GetString("email")+":25.0,50,25";//접속한 유저의 아이디와 생성할 위치를 서버에 전송
		SocketStarter.Socket.Emit("createPlayerREQ",data);
		yield return null;
	}
	
	private string id; //접속한 유저의 아이디

	// Use this for initialization
	void Start () {
		ClientID = ClientState.id;
		//StartCoroutine (CreatePlayer());
		spawnSwitch = false;
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
			//a.GetComponentInChildren<HP_Bar>().target = a.transform;
			spawnSwitch=false;
		}
	}
}
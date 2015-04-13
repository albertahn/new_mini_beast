using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
	public GameObject player;
	public bool spawnSwitch;//socket.on에서 instantiate를 하면 에러가 나서 업데이트 문에서 쓰기위한 꼼수
	Vector3 spawnPos;
	private string ClientID;//접속한 사람의 id	

	public IEnumerator CreatePlayer(){
		spawnSwitch = false;

		string data = ClientState.id+":25.0,50,25:"+ClientState.character;//접속한 유저의 아이디와 생성할 위치를 서버에 전송
		SocketStarter.Socket.Emit("createPlayerREQ",data);
		yield return null;
	}
	
	private string id; //접속한 유저의 아이디
	public string character;

	// Use this for initialization
	void Start () {
		ClientID = ClientState.id;

		PlayerPrefs.SetString("evolved", "false");
		//StartCoroutine (CreatePlayer());
		spawnSwitch = false;
	}

	public void setSpawn(string _id,Vector3 pos,string _char){
		id = _id;
		spawnPos = pos;
		character = _char;
		spawnSwitch=true;
	}

	// Update is called once per frame
	void Update () {
		if (spawnSwitch) {
			GameObject a;
			player = (GameObject)Resources.Load(character);
			a = (GameObject)Instantiate(player,spawnPos,Quaternion.identity);
			a.name=id;
			//a.GetComponentInChildren<HP_Bar>().target = a.transform;
			spawnSwitch=false;
		}
	}
}
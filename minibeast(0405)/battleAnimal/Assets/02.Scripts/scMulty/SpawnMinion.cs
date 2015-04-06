using UnityEngine;
using System.Collections;

public class SpawnMinion : MonoBehaviour {
	public GameObject minion;
	private Vector3 spawnPos;
	public bool spawnSwitch;
	private string id;
	public GameObject rms;
	public GameObject bms;

	// Use this for initialization
	void Start () {
		spawnSwitch = false;
		rms = GameObject.Find ("redMinions");
		bms = GameObject.Find ("blueMinions");
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnSwitch) {
			GameObject a;
			a = (GameObject)Instantiate(minion,spawnPos,Quaternion.identity);
			a.name=id;
			a.transform.parent = rms.transform;
			spawnSwitch = false;
			if(ClientState.isMaster){//edit
				a.GetComponent<minionCtrl>().isMaster = true;
			}
		}
	
	}

	public void setSpawn(string _id,Vector3 _data){
		id = _id;
		spawnPos = _data;
		spawnSwitch=true;
	}
}

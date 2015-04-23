using UnityEngine;
using System.Collections;

public class moveSync: MonoBehaviour {
	public Transform tr;
	public float duration;
	public float startTime;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();

		duration = 0.5f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (ClientState.id == gameObject.name) {
				if(Time.time-startTime>duration){
					//Debug.Log ("it's different");
					string data = ClientState.id+":"+tr.position.x+","+tr.position.y+","+tr.position.z;
					SocketStarter.Socket.Emit("moveSyncREQ",data);
					startTime =Time.time;
				}
		}*/
	}
}
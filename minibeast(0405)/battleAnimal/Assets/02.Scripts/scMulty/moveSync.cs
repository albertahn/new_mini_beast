using UnityEngine;
using System.Collections;

public class moveSync: MonoBehaviour {
	public Transform tr;
	public Vector3 pre_tr;
	public float duration;
	public float startTime;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		pre_tr = t2v (tr);

		duration = 0.5f;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {		
		if (ClientState.id == gameObject.name) {
			if(!isEqual(tr.position,pre_tr)){
				if(Time.time-startTime>duration){
					//Debug.Log ("it's different");
					string data = ClientState.id+":"+tr.position.x+","+tr.position.y+","+tr.position.z;
					SocketStarter.Socket.Emit("moveSyncREQ",data);
					startTime =Time.time;
				}
				pre_tr = t2v (tr);
			}
		}
	}
	
	Vector3 t2v(Transform t){		
		Vector3 a;
		a.x = t.position.x;
		a.y = t.position.y;
		a.z = t.position.z;
		return a;
	}

	bool isEqual(Vector3 a,Vector3 b){
		int ax = (int)a.x;
		int ay = (int)a.y;
		int az = (int)a.z;

		int bx = (int)b.x;
		int by = (int)b.y;
		int bz = (int)b.z;

		if (ax == bx && ay == by && az == bz)
						return true;
				else
						return false;
	}
}
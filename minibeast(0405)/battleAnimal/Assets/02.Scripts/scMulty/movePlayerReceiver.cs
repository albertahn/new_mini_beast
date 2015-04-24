using UnityEngine;
using System.Collections;

public class movePlayerReceiver : MonoBehaviour {
	private bool switch_;
	private string id;
	private Vector3 pos;	
	private MoveCtrl _moveCtrl;

	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (switch_) {
			//run = true;
			GameObject a = GameObject.Find (id);

			if (a != null) {
				_moveCtrl = a.GetComponent<MoveCtrl>();
				_moveCtrl.clickendpoint= pos;
				_moveCtrl.move();
			}

			switch_ = false;
		}
	}

	public void receive(string data){
		string[] temp = data.Split (':');
		string[] posTemp;
		
		while (switch_) {}
		id = temp [0];
		posTemp = temp [1].Split (',');
		pos = new Vector3(float.Parse(posTemp[0]),
		                  float.Parse(posTemp[1]),
		                  float.Parse(posTemp[2]));
		switch_ = true;
	}
}
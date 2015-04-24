using UnityEngine;
using System.Collections;

public class playerAttackReceiver : MonoBehaviour {
	private bool switch_;
	private string attacker;
	private string target;
	private MoveCtrl _moveCtrl;

	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			GameObject a = GameObject.Find (attacker);
			
			if (a != null) {
				Debug.Log ("hihi");
				_moveCtrl = a.GetComponent<MoveCtrl>();
				_moveCtrl.attack(target);
			}

			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');
		while (switch_) {}
		attacker = temp [0];
		target = temp [1];
		Debug.Log("attacker = "+attacker+" target = "+target);
		switch_ = true;
	}
}

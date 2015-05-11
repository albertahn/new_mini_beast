using UnityEngine;
using System.Collections;

public class BlueCannon_OutterCtrl : MonoBehaviour {
	public BlueCannonCtrl _ctrl;
	private string targetName;
	
	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<BlueCannonCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Player") {
			string parentName = coll.gameObject.transform.parent.name;
			if (parentName [0] == 'R') {
				targetName = coll.name;
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		} else if (coll.tag == "MINION") {
			if(coll.name[0] =='r'){
				targetName = coll.name;
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		}else if (coll.tag == "BUILDING") {		
			if (coll.name [0] == 'r') {
				targetName = coll.name;
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		}
	}
	void OnTriggerExit(Collider coll){
		if (coll.name == targetName)
			_ctrl.isAttack = false;
	}
}

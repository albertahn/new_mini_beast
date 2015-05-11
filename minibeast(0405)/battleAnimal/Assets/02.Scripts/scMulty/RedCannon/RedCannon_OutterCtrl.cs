using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedCannon_OutterCtrl : MonoBehaviour {
	public RedCannonCtrl _ctrl;
	private string targetName;

	public List<GameObject> enemyList;

	
	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<RedCannonCtrl> ();
		enemyList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Player") {
			string parentName = coll.gameObject.transform.parent.name;
			if (parentName [0] == 'B') {
				targetName = coll.name;
				enemyList.Add(coll.gameObject);
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		} else if (coll.tag == "MINION") {
			if(coll.name[0] =='b'){
				targetName = coll.name;
				enemyList.Add(coll.gameObject);
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		}else if (coll.tag == "BUILDING") {		
			if (coll.name [0] == 'b') {
				targetName = coll.name;
				enemyList.Add(coll.gameObject);
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		}
	}

	void OnTriggerExit(Collider coll){
		if (coll.name == targetName)
		//	enemyList.BinarySearch (coll.gameObject);
						_ctrl.isAttack = false;
	}
}

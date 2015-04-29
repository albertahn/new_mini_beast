using UnityEngine;
using System.Collections;

public class RedCannon_OutterCtrl : MonoBehaviour {
	public RedCannonCtrl _ctrl;
	
	// Use this for initialization
	void Start () {
		_ctrl = GetComponentInParent<RedCannonCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Player") {
			string parentName = coll.gameObject.transform.parent.name;
			if (parentName [0] == 'B') {
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		} else if (coll.tag == "MINION") {
			if(coll.name[0] =='b'){
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		}else if (coll.tag == "BUILDING") {		
			if (coll.name [0] == 'b') {
				_ctrl.targetObj = coll.gameObject;
				_ctrl.isAttack = true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class RedCannonCtrl : MonoBehaviour {
	private RedCannonFire _fireCtrl;
	public bool isAttack;
	public GameObject targetObj;

	private Transform tr;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		isAttack = false;
		_fireCtrl = GetComponent<RedCannonFire> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack) {
			if (targetObj != null) {
				if(targetObj.tag=="Player"&&targetObj.GetComponent<PlayerHealthState>().isDie==true){
					isAttack=false;
				}else if(targetObj.tag=="MINION"&&targetObj.GetComponent<blueMinionCtrl>().isDie==true){
					isAttack=false;
				}

				_fireCtrl.Fire (targetObj.name);
			}
		}
	}
}

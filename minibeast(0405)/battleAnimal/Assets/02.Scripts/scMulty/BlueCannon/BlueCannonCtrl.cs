using UnityEngine;
using System.Collections;

public class BlueCannonCtrl : MonoBehaviour {
	private BlueCannonFire _fireCtrl;
	public bool isAttack;
	public GameObject targetObj;

	private Transform tr;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		isAttack = false;
		_fireCtrl = GetComponent<BlueCannonFire> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack) {
			if (targetObj != null) {
				if(targetObj.tag=="Player"&&targetObj.GetComponent<PlayerHealthState>().isDie==true){
					isAttack=false;
				}else if(targetObj.tag=="MINION"&&targetObj.GetComponent<minionCtrl>().isDie==true){
					isAttack=false;
				}
				_fireCtrl.Fire (targetObj.name);
			}
		}
	}
}

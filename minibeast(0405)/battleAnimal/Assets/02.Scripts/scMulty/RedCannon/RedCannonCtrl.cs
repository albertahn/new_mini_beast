using UnityEngine;
using System.Collections;

public class RedCannonCtrl : MonoBehaviour {
	private RedCannonFire _fireCtrl;
	public bool isAttack;
	public GameObject targetObj;
	private float attackDist;

	private Transform tr;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		attackDist = 20.0f;
		isAttack = false;
		_fireCtrl = GetComponent<RedCannonFire> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isAttack) {
			if (targetObj != null) {
				if(targetObj.tag=="Player"&&targetObj.GetComponent<PlayerHealthState>().isDie==true){
					isAttack=false;
					Debug.Log ("11");
				}else if(targetObj.tag=="MINION"&&targetObj.GetComponent<blueMinionCtrl>().isDie==true){
					isAttack=false;
					Debug.Log ("22");
				}
				if(Vector3.Distance(targetObj.transform.position,tr.position)>=attackDist){
					isAttack=false;
					Debug.Log ("33");
				}
				_fireCtrl.Fire (targetObj.name);
			}
		}
	}
}

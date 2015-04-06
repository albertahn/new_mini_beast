using UnityEngine;
using System.Collections;

public class MainFortress : MonoBehaviour {


	public GameObject bloodEffect;
	public GameObject bloodDecal;

	private int hp = 400;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



	
	}


	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "BULLET_BALL") {

			Debug.Log("building hit!");


			StartCoroutine (this.CreateBloodEffect(coll.transform.position));
			
		/*	if(coll.gameObject.tag == "BULET_BALL")
				hp -= coll.gameObject.GetComponent<ArrowCtrl>().damage;
			else 
				hp -= coll.gameObject.GetComponent<preArrowCtrl>().damage;
			if(hp<=0)
			{
				MonsterDie();
			}
		*/	

		}
	}


	IEnumerator CreateBloodEffect(Vector3 pos)
	{
		GameObject _blood1 = (GameObject)Instantiate (bloodEffect, pos, Quaternion.identity);
		Destroy (_blood1, 2.0f);
		
		Vector3 decalPos = this.transform.position+(Vector3.right*5.01f);
		Quaternion decalRot = Quaternion.Euler(0,Random.Range(0,360),0);
		
		GameObject _blood2 = (GameObject)Instantiate (bloodDecal, decalPos, decalRot);
		float _scale = Random.Range (1.5f, 3.5f);
		_blood2.transform.localScale = new Vector3 (_scale, 1, _scale);
		Destroy (_blood2, 5.0f);
		
		yield return null;
	}
}

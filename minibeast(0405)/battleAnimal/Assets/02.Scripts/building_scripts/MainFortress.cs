using UnityEngine;
using System.Collections;

public class MainFortress : MonoBehaviour {


	public GameObject bloodEffect;
	public GameObject bloodDecal;

	public int hp = 400;

	public bool buildingDead;

	// Use this for initialization
	void Start () {
	
		buildingDead = false;

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){


		if (this.gameObject.name == "blue_building" && buildingDead ==true ) {
		
			if (GUI.Button (new Rect (10, 100, 450, 300), "Red Team Win")) {
				
				Application.LoadLevel ("scStart");
				
			}
		}else if(this.gameObject.name == "red_building" && buildingDead==true){

			if (GUI.Button (new Rect (10, 100, 450, 300), "Blue Team Win")) {
				
				Application.LoadLevel ("scStart");
				
			}
		}
	}


	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "BULLET_BALL") {
			StartCoroutine (this.CreateBloodEffect(coll.transform.position));
				hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
				string data = this.name+":" + hp.ToString()+"";
				SocketStarter.Socket.Emit ("attackBuilding", data);
			if(hp<=0)
			{
				hp=0;
				buildingDie();
			}
		}else if(coll.gameObject.tag == "M_BULLET_BALL"){
			StartCoroutine (this.CreateBloodEffect(coll.transform.position));
			hp -= coll.gameObject.GetComponent<mBulletCtrl>().damage;
			string data = this.name+":" + hp.ToString()+"";
			SocketStarter.Socket.Emit ("attackBuilding", data);
			if(hp<=0)
			{
				hp=0;
				buildingDie();
			}
		}else if(coll.gameObject.tag == "SKILL_FIRST"){

			StartCoroutine (this.CreateBloodEffect(coll.transform.position));
			hp -= coll.gameObject.GetComponent<SkillFirstCrl>().damage;
			string data = this.name+":" + hp.ToString()+"";
			SocketStarter.Socket.Emit ("attackBuilding", data);
			if(hp<=0)
			{
				hp=0;
				buildingDie();
			}

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

	public void buildingDie(){

		buildingDead = true;

	}
}

using UnityEngine;
using System.Collections;

public class PlayerHealthState : MonoBehaviour {


	public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	public int hp = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Heated(GameObject obj){
		Collider coll = obj.collider;
		
		StartCoroutine (this.CreateBloodEffect(coll.transform.position));


		Debug.Log ("playerhp: "+hp);
		
		
		hp -= obj.GetComponent<BulletCtrl>().damage;
		
		string data = this.name+":" + hp.ToString()+"";
		SocketStarter.Socket.Emit ("attackMinion", data);			
		
		if(hp<=0)
		{
			hp=0;
			playerDie();
		}
		
		Destroy (obj.gameObject);
	}//end heated


	void playerDie(){
		this.collider.enabled = false;
		GetComponent<minionCtrl> ().isDie = true;
		
		int oldInt = PlayerPrefs.GetInt ("minions_killed");
		
		PlayerPrefs.SetInt ("minions_killed",oldInt+1);
		
		/*if(PlayerPrefs.GetInt ("minions_killed") >1  && PlayerPrefs.GetString("evolved")=="false"){
			
			
			GameObject.Find (ClientState.id).GetComponent<Level_up_evolve>().switchToEvol=true;
			
			//PlayerPrefs.SetString("evolved", "true");
			
		}*/
		Destroy (this.gameObject, 3.0f);
		
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

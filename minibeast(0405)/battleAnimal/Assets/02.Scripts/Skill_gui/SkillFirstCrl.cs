using UnityEngine;
using System.Collections;

public class SkillFirstCrl : MonoBehaviour {

	public int damage;
	private float speed;
	public float birth;
	private float durationTime;
	public string firedByName;
	
	// Use this for initialization
	void Start () {
		damage = 120;
		speed = 1000.0f;
		rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime)
			Destroy (this.gameObject);	
	}

	public void shotByname(string firedBy){
		firedByName = firedBy;
	
	}

	void OnTriggerEnter(Collider coll){

	

	

		if (coll.gameObject.tag == "MINION") {
			string parentName = coll.gameObject.transform.parent.name;

			if(ClientState.team=="red"&&parentName=="BlueTeam"
			   ||ClientState.team=="blue"&&parentName=="RedTeam"){

			Debug.Log("skill first hit min");

			coll.gameObject.GetComponent<minion_state>().Heated("skill", gameObject);
			Destroy (this.gameObject);
			}
		}else if(coll.gameObject.tag=="Player"){
			string parentName = coll.gameObject.transform.parent.name;

							if(ClientState.team=="red"&&parentName=="BlueTeam"
							   ||ClientState.team=="blue"&&parentName=="RedTeam"){
							Debug.Log("hitskill");
						
							coll.gameObject.GetComponent<PlayerHealthState>().hitbySkill(firedByName, this.gameObject);
						Destroy (this.gameObject);
			       }//if

	}//hit player



	}//end colide


}

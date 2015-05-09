using UnityEngine;
using System.Collections;

public class Skill_socket_reciever : MonoBehaviour {

	// Use this for initialization

	public bool firedskill;

	public string userID;

	public Vector3 newPos;

	public FireSkill skillfire;
	void Start () {
	
		firedskill = false;

	}
	
	// Update is called once per frame
	void Update () {

//show the skill to other player

		if(firedskill ==true){

			//if my skill dont show, else show

			GameObject firedplayer = GameObject.Find(userID);

			firedplayer.transform.LookAt(newPos);
			
			skillfire = firedplayer.GetComponent<FireSkill> ();					
			//skillfire.Fireman(userID);

			firedskill = false;

		}
	
	}

	public void skillShot(string data){



		string[] temp = data.Split(':');
		string username = temp[0];
		string  building_hp_int = temp[1];

		userID = temp[0];
		string[] resPos = temp[1].Split(',');
		newPos = new Vector3(float.Parse(resPos[0]),
		                     float.Parse(resPos[1]),
		                     float.Parse(resPos[2]));


		Debug.Log("attack: skill " + username+":"+building_hp_int);

		firedskill = true;

		Debug.Log ("data fied: "+firedskill);

	}//
}

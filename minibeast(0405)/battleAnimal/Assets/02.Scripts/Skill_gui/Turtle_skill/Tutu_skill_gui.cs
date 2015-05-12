﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutu_skill_gui : MonoBehaviour {

	// Use this for initia
	//public Texture2D stexture1, stexture2 , stexture3 ;
	private Transform trans;
	private string ClientID;
	
	public Image[] skills;
	public Sprite skill1_spr,skill2_spr,skill3_spr;
	public Sprite skill1Blank_spr,skill2Blank_spr,skill3Blank_spr;
	
	
	public int xfirst, yfirst, xsecond, ysecond, xthird, ythird, xfourth ;	
	public MoveCtrl myMoveCtrl;
	
	public GameObject firstskill, secondskill, thirdskill;
	public GUILayer guilayer;
	
	//public FireSkill skillfire;
	
	public bool skillOneReady =false;
	public bool skillTwoReady =false;
	public bool skillThreeReady = false;
	
	public bool[] skill_state;
	public bool[] skill_live;
	private Level_up_evolve _lvUpEvolve;
	
	private float[] skillCool;
	private float[] skillStartTime;
	
	// Use this for initialization
	void Start (){
		
		ClientID = ClientState.id;
		
		//Get game object
		myMoveCtrl = GetComponent<MoveCtrl> ();
		guilayer = Camera.main.GetComponent<GUILayer>();
		
		trans = GetComponent<Transform>();
		
		skills = GameObject.Find ("skillWindow").GetComponentsInChildren <Image>();
		
		skill_state = new bool[3];
		for (int i=0; i<3; i++)
			skill_state [i] = false;
		
		skillCool = new float[3];
		skillCool [0] = 5.0f;
		skillCool [1] = 5.0f;
		skillCool [2] = 15.0f;
		
		skillStartTime = new float[3];
		
		skill_live = new bool[3];
		for (int i=0; i<3; i++) {
			skill_live [i] = false;//false;
		}
		_lvUpEvolve =  GetComponent<Level_up_evolve> ();
		
	}
	
	
	
	public void Skill1_bot()
	{
		
		Debug.Log ("what the fuck");
		
		if (skill_state [0]&&Time.time-skillStartTime[0]>=skillCool[0]) {
			GameObject dogy = GameObject.Find (ClientState.id);
			
			Debug.Log ("skill1bot: "+ClientID);
			
			Vector3 spawnPos = dogy.transform.position;
			Quaternion rotationdog = dogy.transform.rotation;
			
			GameObject a;
			a = (GameObject)Instantiate (firstskill, spawnPos, rotationdog);
			a.name = "firstskill";
			
			a.transform.parent = dogy.transform;	
			skillOneReady = true;
			skillStartTime[0] = Time.time;
			skill_state [0] = false;
			skills [0].sprite = skill1Blank_spr;
		}
	}
	
	public void Skill2_bot()
	{
		if (skill_state [1]&&Time.time-skillStartTime[1]>=skillCool[1]) {		
			Debug.Log ("clicked 2 man");
			GameObject dogy = GameObject.Find (ClientID);
			
			Vector3 spawnPos = dogy.transform.position;
			Quaternion rotationdog = dogy.transform.rotation;
			
			GameObject a;
			a = (GameObject)Instantiate (secondskill, spawnPos, rotationdog);
			a.name = "secondskill";
			
			a.transform.parent = dogy.transform;
			skillTwoReady = true;
			skillStartTime[1] = Time.time;
			skill_state [1] = false;
			skills [1].sprite = skill2Blank_spr;
		}
	}
	
	
	public void Skill3_bot()
	{
		if (skill_state [2]&& Time.time-skillStartTime[2] >= skillCool[2]) {
		GameObject dogy = GameObject.Find (ClientState.id);
		
		//Debug.Log ("client id : "+ClientID);
		
		Vector3 spawnPos = dogy.transform.position;
		Quaternion rotationdog = dogy.transform.rotation;
		
		GameObject a;
		a = (GameObject)Instantiate (firstskill, spawnPos, rotationdog);
		a.name = "thirdskill";
		
		a.transform.parent = dogy.transform;	
		skillThreeReady = true;
		skillStartTime[2] = Time.time;
		skill_state [2] = false;
		skills [2].sprite = skill1Blank_spr;
		}
	}
	
	
	public void skill1Plus_bot(){
		skills [0].sprite = skill1_spr;
		skill_state [0] = true;
		skill_live [0] = true;
		ClientState.skillPoint--;
		if(ClientState.skillPoint<=0)
			_lvUpEvolve.closeSkillPlus ();
		
	}
	
	public void skill2Plus_bot(){
		skills [1].sprite = skill2_spr;
		skill_state [1] = true;
		skill_live [1] = true;
		ClientState.skillPoint--;
		if(ClientState.skillPoint<=0)
			_lvUpEvolve.closeSkillPlus ();
	}
	
	public void skill3Plus_bot(){
		skills [2].sprite = skill3_spr;
		skill_state [2] = true;
		skill_live [2] = true;
		ClientState.skillPoint--;
		if(ClientState.skillPoint<=0)
			_lvUpEvolve.closeSkillPlus ();
	}
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit hitman;
		
		if (skill_live[0]&&Time.time - skillStartTime [0] >= skillCool [0]) {
			skills [0].sprite = skill1_spr;
			skill_state [0] = true;
		}
		
		if (skill_live[1]&&Time.time - skillStartTime [1] >= skillCool [1]) {
			skills [1].sprite = skill2_spr;
			skill_state [1] = true;
		}
		
		if (skill_live[2]&&Time.time - skillStartTime [2] >= skillCool [2]) {
			skills [2].sprite = skill3_spr;
			skill_state [2] = true;
		}
		
		if (Input.GetMouseButtonDown (0) ) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			
			RaycastHit hiterone;
			
			//GUIElement	hitObject = guilayer.HitTest( Input.mousePosition );if (Physics.Raycast (ray, out hitman, Mathf.Infinity)) {
			
			if (Physics.Raycast (ray, out hiterone, Mathf.Infinity, 1<<LayerMask.NameToLayer("FLOOR"))) { 
				
				
				if(skillOneReady ==true){
					
					Debug.Log("first skill fired");
					
					GameObject dog =  GameObject.Find(ClientState.id);

					firedFirst(dog, hiterone.point, ClientState.id);
					
					/*dog.transform.LookAt(hiterone.point);
					
					swirl_sword wingskill = dog.GetComponent<swirl_sword> ();	
					wingskill.swirlIt(ClientState.id);
					//destroy gameobject]
					//destroy all wraps
					clearSkillWraps();
					
					skillOneReady = false;*/
					
					
					Vector3	clickendpoint= hiterone.point;
					string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z +":"+ ClientState.character +":first";
					
					SocketStarter.Socket.Emit ("SkillAttack", data);  //내위치를 서버에 알린다.				
					
					
				}//skill 1 ready true
				
				if(skillTwoReady == true){
					
					Debug.Log("sec skill fired");
					
					GameObject dog =  GameObject.Find(ClientID);
					
					dog.transform.LookAt(hiterone.point);
					
					Vector3 clickendpoint = hiterone.point;
					float step = 350 * Time.deltaTime;
					dog.transform.position = Vector3.MoveTowards(dog.transform.position, clickendpoint, step);
					
					skillTwoReady = false;
					
					GameObject skill1 =  GameObject.Find("secondskill");
					Destroy (skill1);	
				}
				
				
				
				if(skillThreeReady){
					
					Debug.Log("3 skill fired");
					
					//	Debug.Log("fired: skill "+skillfire.ToString());
					GameObject dog =  GameObject.Find(ClientState.id);

					firedThird(dog, hiterone.point, ClientState.id);
					

					
					Vector3	clickendpoint= hiterone.point;
					string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z +":"+ ClientState.character +":third";
					
					SocketStarter.Socket.Emit ("SkillAttack", data);  //내위치를 서버에 알린다.				
					
					
				}//skill 1 ready true
				
				
			} ///raycasr
			
		}//mousedown skillone ready 
		
		
	}//end yupdate
	
	
	void clearSkillWraps(){
		
		GameObject[] skillwraps =  GameObject.FindGameObjectsWithTag("SKILL_WRAP");//Find("firstskill");
		
		for (var i = 0; i <  skillwraps.Length; i ++) {
			
			Debug.Log("die");
			
			Destroy (skillwraps [i]);	
		}		
		
	}//clear

//all fired skills

	public void firedFirst(GameObject gameobject, Vector3 vector, string firedby){

		gameobject.transform.LookAt(vector);
		
		swirl_sword wingskill = gameobject.GetComponent<swirl_sword> ();	
		wingskill.swirlIt(firedby);

		clearSkillWraps();
		
		skillOneReady = false;
	} 



	public void firedThird(GameObject gameobject, Vector3 vector, string firedby){
		
		gameobject.transform.LookAt(vector);
		
		fireShell skillfire =  gameobject.GetComponent<fireShell> ();	
		skillfire.fireBall(firedby);
		//destroy gameobject]
		//destroy all wraps
		clearSkillWraps();
		
		skillThreeReady = false;

	} 
}
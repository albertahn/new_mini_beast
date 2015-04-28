using UnityEngine;
using System.Collections;

public class DogSkill_GUI : MonoBehaviour {
	
	public Texture2D stexture1, stexture2 , stexture3 ;
	private Transform trans;
	private string ClientID;

	
	public int xfirst, yfirst, xsecond, ysecond, xthird, ythird, xfourth ;
	
	public MoveCtrl myMoveCtrl;
	
	public GameObject firstskill, secondskill, thirdskill;
	public GUILayer guilayer;
	
	public FireSkill skillfire;
	
	public bool skillOneReady =false;
	public bool skillTwoReady =false;
	
	// Use this for initialization
	void Start () {
		ClientID = ClientState.id;
		
		//Get game object
		myMoveCtrl = GetComponent<MoveCtrl> ();
		guilayer = Camera.main.GetComponent<GUILayer>();
		
		trans = GetComponent<Transform> ();
		skillfire = GetComponent<FireSkill> ();
	}
	
	
	void OnGUI(){
	

	}//end gui

	public void Skill1_bot()
	{		
		GameObject dogy =  GameObject.Find(ClientID);
		
		Vector3 spawnPos = dogy.transform.position;
		Quaternion rotationdog = dogy.transform.rotation;
		
		GameObject a;
		a = (GameObject) Instantiate(firstskill, spawnPos ,rotationdog);
		a.name="firstskill";
		
		a.transform.parent = dogy.transform;	
		skillOneReady = true;
	}

	public void Skill2_bot()
	{
		
		Debug.Log("clicked 2 man");
		GameObject dogy =  GameObject.Find(ClientID);
		
		Vector3 spawnPos = dogy.transform.position;
		Quaternion rotationdog = dogy.transform.rotation;
		
		GameObject a;
		a = (GameObject) Instantiate(secondskill, spawnPos ,rotationdog);
		a.name="secondskill";
		
		a.transform.parent = dogy.transform;
		skillTwoReady = true;
	}

	// Update is called once per frame
	void Update () {
		
		RaycastHit hitman;
		
		
		if (Input.GetMouseButtonDown (0) ) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			
			RaycastHit hiterone;
			
			//GUIElement	hitObject = guilayer.HitTest( Input.mousePosition );if (Physics.Raycast (ray, out hitman, Mathf.Infinity)) {
			
			if (Physics.Raycast (ray, out hiterone, Mathf.Infinity, 1<<LayerMask.NameToLayer("FLOOR"))) { 
				
				
				if(skillOneReady ==true){

					//Debug.Log(""+skillfire.ToString());
					GameObject dog =  GameObject.Find(ClientID);
					
					dog.transform.LookAt(hiterone.point);
					
					skillfire = dog.GetComponent<FireSkill> ();	
					Debug.Log("this name = "+this.name);
					skillfire.Fireman(ClientState.id);
					//destroy gameobject]
					//destroy all wraps
					clearSkillWraps();
					
					skillOneReady = false;
			

					Vector3	clickendpoint= hiterone.point;
					string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;

					SocketStarter.Socket.Emit ("SkillAttack", data);  //내위치를 서버에 알린다.				
					
					
				}//skill 1 ready true
				
				if(skillTwoReady == true){

					GameObject dog =  GameObject.Find(ClientID);
					
					dog.transform.LookAt(hiterone.point);
					
					Vector3 clickendpoint = hiterone.point;
					float step = 350 * Time.deltaTime;
					dog.transform.position = Vector3.MoveTowards(dog.transform.position, clickendpoint, step);
					
					skillTwoReady = false;
					
					GameObject skill1 =  GameObject.Find("secondskill");
					Destroy (skill1);	
				}
				
				
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
}

using UnityEngine;
using System.Collections;

public class DogSkill_GUI : MonoBehaviour {
	
	public Texture2D stexture1, stexture2 , stexture3 ;
	private Transform trans;
	
	
	
	public int xfirst, yfirst, xsecond, ysecond, xthird, ythird;
	
	public MoveCtrl myMoveCtrl;
	
	public GameObject firstskill, secondskill, thirdskill;
	public GUILayer guilayer;
	
	public FireSkill skillfire;
	
	public bool skillOneReady =false;
	public bool skillTwoReady =false;
	
	// Use this for initialization
	void Start () {
		
		xfirst = 10;  yfirst = 400;
		xsecond = 10 + stexture1.width;
		xthird = 10 + stexture1.width + stexture2.width;
		
		//Get game object
		myMoveCtrl = GameObject.Find(PlayerPrefs.GetString ("email")).GetComponent<MoveCtrl> ();
		guilayer = Camera.main.GetComponent<GUILayer>();
		
		trans = GameObject.Find(PlayerPrefs.GetString ("email")).GetComponent<Transform> ();
		skillfire = GameObject.Find(PlayerPrefs.GetString ("email")).GetComponent<FireSkill> ();
	}
	
	
	void OnGUI(){
		
		if (GUI.Button (new Rect (xfirst, yfirst, stexture1.width, stexture1.height), stexture1)) {
			
			
			skillOneReady = true;
			
			GameObject dog =  GameObject.Find(PlayerPrefs.GetString("email"));
			
			Vector3 spawnPos = dog.transform.position;
			Quaternion rotationdog = dog.transform.rotation;
			
			GameObject a;
			a = (GameObject) Instantiate(firstskill, spawnPos ,rotationdog);
			a.name="firstskill";
			
			a.transform.parent = dog.transform;
			
			
			
			
			//fixiate dog  select area to shoot  playermoving =false
			
		}
		
		
		if (GUI.Button (new Rect (xsecond, yfirst, stexture2.width, stexture2.height), stexture2)) {
			
			skillTwoReady = true;
			
			Debug.Log("clicked 2 man");
			GameObject dog =  GameObject.Find(PlayerPrefs.GetString("email"));
			
			Vector3 spawnPos = dog.transform.position;
			Quaternion rotationdog = dog.transform.rotation;
			
			GameObject a;
			a = (GameObject) Instantiate(secondskill, spawnPos ,rotationdog);
			a.name="secondskill";
			
			a.transform.parent = dog.transform;
			
			
		}
		
		if (GUI.Button (new Rect (xthird, yfirst, stexture3.width, stexture3.height), stexture3)) {
			
			Debug.Log("clickedman");
			
			//myMoveCtrl.skill_touch = true;
			
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit hitman;
		
		
		if (Input.GetMouseButtonDown (0) ) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			
			RaycastHit hiterone;
			
			//GUIElement	hitObject = guilayer.HitTest( Input.mousePosition );if (Physics.Raycast (ray, out hitman, Mathf.Infinity)) {
			
			if (Physics.Raycast (ray, out hiterone, Mathf.Infinity) && hiterone.collider.tag == "FLOOR" ) {
				
				
				if(skillOneReady ==true){ 
					
					//Debug.Log(""+skillfire.ToString());
					GameObject dog =  GameObject.Find(PlayerPrefs.GetString("email"));
					
					dog.transform.LookAt(hiterone.point);
					
					skillfire = dog.GetComponent<FireSkill> ();
					
					skillfire.Fireman();
					
					//destroy gameobject]
					
					skillOneReady = false;
					
					GameObject skill1 =  GameObject.Find("firstskill");
					Destroy (skill1);	
					
					
					
				}//skill 1 ready true
				
				
				if(skillTwoReady == true){
					
					
					GameObject dog =  GameObject.Find(PlayerPrefs.GetString("email"));
					
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
		
		
		
		
		
	}
}

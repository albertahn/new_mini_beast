using UnityEngine;
using System.Collections;

public class MoveCtrl : MonoBehaviour {	
	private Transform tr;
	Vector3 pre_tr;
	private CharacterController _controller;
	private FireCtrl _fireCtrl;
	
	public float h = 0.0f;
	public float v = 0.0f;
	
	public float movSpeed = 5.0f;
	public float rotSpeed = 50.0f;
	
	private Vector3 movDir = Vector3.zero;	
	private string ClientID;	
	public float myypos, myxpos,myzpos;	
	
	public Vector3 clickendpoint;	
	public bool playermoving = false;
	
	private bool screenmoveonly;
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;
	
	public float timeOfTouch;

	public bool isAttack;
	public Vector3 attackPoint;
	
	public bool isMoveAndAttack;

	public GameObject targetObj;

	public bool swiped;

	
	// Use this for initialization
	void Start () {
		attackPoint = Vector3.zero;
		tr = this.GetComponent<Transform> ();
		_fireCtrl = this.GetComponent<FireCtrl> ();
		pre_tr = t2v(tr);
		_controller = GetComponent<CharacterController> ();
		ClientID = ClientState.id;		
		
		myxpos = tr.transform.position.x;
		myypos = tr.transform.position.y;
		
		directionChosen = false;
		isAttack = false;
		isMoveAndAttack=false;

		swiped = false;
	}	

	// Update is called once per frame
	void Update () {
		if (ClientID == gameObject.name) {

			//id가 내 캐릭터 일때
			#if UNITY_ANDROID||UNITY_IPHONE
			if (Input.touchCount == 1 && Input.touchCount  <2) {
				
				var touch = Input.GetTouch(0);				
				
				switch (touch.phase) {
					// Record initial touch position.
				case TouchPhase.Began:
					timeOfTouch = Time.time;

					swiped = false;

					break;					
					// Determine direction by comparing the current touch position with the initial one.
				case TouchPhase.Moved:
					direction = touch.position - startPos;

					swiped = true;

				Debug.Log("swiped");

					break;					
					// Report that a direction has been chosen when the finger is lifted.
				case TouchPhase.Ended:		

					if(Time.time - timeOfTouch>3.0f || swiped ==true){
						directionChosen = true;	

					}else{
						directionChosen = false;	

						swiped = false;
						
					
					}	//				
					break;
				}//end switch

			}

			if (Input.touchCount == 1  && Input.GetTouch(0).phase != TouchPhase.Moved  && directionChosen ==false) {
			Ray ray3 = Camera.main.ScreenPointToRay (Input.touches [0].position);
			RaycastHit hit3;				
			if(Physics.Raycast(ray3, out hit3, Mathf.Infinity, 1<<LayerMask.NameToLayer("FLOOR"))){//&& hit3.collider.tag=="FLOOR"){
				
				Vector3 target = new Vector3(hit3.point.x, 0 , hit3.point.z);
				
				clickendpoint = hit3.point;
				
				//mark the plack  moveToPointMark
				//moveToPointMark(clickendpoint);

					string data = ClientID + ":" +tr.position.x+","+tr.position.y+","+tr.position.z+
						":"+ clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
					SocketStarter.Socket.Emit ("movePlayerREQ", data);//내위치를 서버에 알린다.		
				
			/*	string data = ClientID + ":" + clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
				
				SocketStarter.Socket.Emit ("movePlayerREQ", data);
				*/
				move();
				
				//playermoving = true;
				
				tr.LookAt(hit3.point); 
				myxpos	=hit3.point.x; //Input.touches [0].position.x;
				myypos	=hit3.point.z;  //Input.touches [0].position.y;	
				
			}else if(hit3.collider.tag =="BUILDING" || hit3.collider.tag =="MINION"||hit3.collider.tag =="Player"){
				string targetName = hit3.collider.name;
				Debug.Log("target = "+targetName);
				Vector3 target = hit3.point;
				target.y=50.0f;
				attackPoint = target;
				
				string data = ClientID + ":" + targetName;
				SocketStarter.Socket.Emit ("attackREQ", data);	
				attack(targetName);								
			}//else hit player
			}//if
				
				
				
				
			//}// if touchcount 1
			#else
		
		
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
						Debug.DrawRay (ray.origin, ray.direction * 100.0f, Color.green);
		
						RaycastHit hitman;
						RaycastHit hitman2;
		
			
			if (Input.GetMouseButtonDown (0)) {	
						if (Physics.Raycast (ray, out hitman, Mathf.Infinity,1<<LayerMask.NameToLayer("FLOOR"))) {

									myxpos = hitman.point.x; //Input.touches [0].position.x;
									myypos = hitman.point.y;  //Input.touches [0].position.y;
									myzpos = hitman.point.z;

									clickendpoint = hitman.point;
						
									string data = ClientID + ":" +tr.position.x+","+tr.position.y+","+tr.position.z+
						":"+ clickendpoint.x + "," + clickendpoint.y + "," + clickendpoint.z;
									SocketStarter.Socket.Emit ("movePlayerREQ", data);//내위치를 서버에 알린다.							
										
									move();
								}
					
					if(Physics.Raycast (ray, out hitman2, Mathf.Infinity)){
						if(hitman2.collider.tag =="BUILDING" || hitman2.collider.tag =="MINION"||hitman2.collider.tag =="Player"){
						string targetName = hitman2.collider.name;
						if(hitman2.collider.tag=="Player"){
							string parentName = hitman2.collider.gameObject.transform.parent.name;

							if(ClientState.team=="red"&&parentName=="BlueTeam"
							   ||ClientState.team=="blue"&&parentName=="RedTeam"){
								Vector3 target = hitman2.point;
								attackPoint = target;

								string data = ClientID + ":" + targetName;
								SocketStarter.Socket.Emit ("attackREQ", data);	
								attack(targetName);
							}
						}else{
							if(ClientState.team=="red"&&targetName[0]=='b'
							   ||ClientState.team=="blue"&&targetName[0]=='r'){
								Vector3 target = hitman2.point;
								attackPoint = target;
							
								string data = ClientID + ":" + targetName;
								SocketStarter.Socket.Emit ("attackREQ", data);	
								attack(targetName);
							}
						}
					}
				}
				} ///raycasr
						#endif
		}
		

		//ifmove
		if (playermoving) {
			tr.LookAt (clickendpoint);
			//if (clickendpoint != tr.position) {
				float step = 5 * Time.deltaTime;
				tr.position = Vector3.MoveTowards(tr.position, clickendpoint, step);
			//}
		}

		if (isAttack) {
			if(targetObj!=null){
				//if(targetObj.GetComponent<minionCtrl>()!=null){
				if(targetObj.tag=="MINION"){
				if(targetObj.name[0]=='b'){
					if(targetObj.GetComponent<blueMinionCtrl>().isDie==true)
					idle ();
					else{
					tr.LookAt (targetObj.transform.position);			
					_fireCtrl.Fire(targetObj.name);

					if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
						clickendpoint=targetObj.transform.position;
						isMoveAndAttack = true;
						playermoving = true;
					}
					}
					}
				}else if(targetObj.name[0]=='r'){
					if(targetObj.GetComponent<minionCtrl>().isDie==true)
						idle ();
					else{
						tr.LookAt (targetObj.transform.position);			
						_fireCtrl.Fire(targetObj.name);
						
						if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
							clickendpoint=targetObj.transform.position;
							isMoveAndAttack = true;
							playermoving = true;
						}
					}
				}else{//non minions
					tr.LookAt (targetObj.transform.position);			
					_fireCtrl.Fire(targetObj.name);
					
					if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
						clickendpoint=targetObj.transform.position;
						isMoveAndAttack = true;
						playermoving = true;
					}
				}//npnmins
			}
		}
		
		if(clickendpoint == tr.position) {
			playermoving = false;
		}

		if (isMoveAndAttack) {
			if(Vector3.Distance (tr.position, targetObj.transform.position) <= _fireCtrl.distance){
				isMoveAndAttack = false;
				attack (targetObj.name);
			}
		}
	}//end update

	public void attack(string _targetName){
		targetObj = GameObject.Find(_targetName);
		if (targetObj != null) {
				if (Vector3.Distance (tr.position, targetObj.transform.position) > _fireCtrl.distance) {
					clickendpoint = targetObj.transform.position;
					isMoveAndAttack = true;
					playermoving = true;
								//moveAndAttack ();
					} else {
						isAttack = true;
						playermoving = false;
			}
		}
	}

	private void moveAndAttack(){
		isMoveAndAttack = true;
		playermoving = true;
	}

	public void move(){
		//mark the plack  moveToPointMark
		moveToPointMark(clickendpoint);

		playermoving = true;
		isAttack = false;
		isMoveAndAttack = false;
	}
	public void idle(){
		playermoving = false;
		isAttack = false;
		isMoveAndAttack = false;
	}
	
	bool isSame(Transform a,Vector3 b){
		if (a.position.x == b.x &&
		    a.position.y == b.y &&
		    a.position.z == b.z)
			return true;
		else
			return false;
	}
	
	Vector3 t2v(Transform t){		
		Vector3 a;
		a.x = t.position.x;
		a.y = t.position.y;
		a.z = t.position.z;
		return a;
	}
	public void moveToPointMark(Vector3 point){

		GameObject pastmovetomark = GameObject.Find ("MoveMark"); 
		
		Destroy (pastmovetomark);

		GameObject movetomark = (GameObject)Resources.Load("moveToMark");
		
		GameObject mark = (GameObject)Instantiate(movetomark,point,Quaternion.identity);
		mark.name="MoveMark";
		
	}


	/*void OnGUI(){

		if (GUI.Button (new Rect (100, 110, 180, 180), "moving: "+playermoving )) {

		}//doanloaded image
	
	}
*/
	//gui test


}
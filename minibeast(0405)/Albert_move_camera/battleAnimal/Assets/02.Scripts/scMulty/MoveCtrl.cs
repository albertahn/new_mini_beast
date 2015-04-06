using UnityEngine;
using System.Collections;

public class MoveCtrl : MonoBehaviour {

	private Transform tr;
	Vector3 pre_tr;
	private CharacterController _controller;

	public float h = 0.0f;
	public float v = 0.0f;

	public float movSpeed = 5.0f;
	public float rotSpeed = 50.0f;

	private Vector3 movDir = Vector3.zero;

	private string ClientID;

	public float myypos, myxpos,myzpos;


	public Vector3 clickendpoint;

	private bool playermoving =false;

	private bool screenmoveonly;
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;

	public float timeOfTouch;

	// Use this for initialization
	void Start () {
		tr = this.GetComponent<Transform> ();
		pre_tr = t2v(tr);
		_controller = GetComponent<CharacterController> ();
		ClientID = ClientState.id;


		myxpos = tr.transform.position.x;
		myypos = tr.transform.position.y;

		directionChosen = false;
	}



	string data;
	// Update is called once per frame
	void Update () {

	

		#if UNITY_ANDROID||UNITY_IPHONE
		if (Input.touchCount == 1 && Input.touchCount > 0) {

			/*Touch touchZero = Input.GetTouch(0);

			Vector2 touchZeroPrevPos = touchZero.position;


			if(Input.GetTouch(0).phase == TouchPhase.Moved && touchZeroPrevPos!= Input.GetTouch(0).position){

				screenmoveonly = true;
			}else{
				screenmoveonly = false;
	
			
			}*/

			if (Input.touchCount > 0) {
				var touch = Input.GetTouch(0);


				switch (touch.phase) {
					// Record initial touch position.
				case TouchPhase.Began:

				

					timeOfTouch = Time.time;
					break;
					
					// Determine direction by comparing the current touch position with the initial one.
				case TouchPhase.Moved:
					direction = touch.position - startPos;
					break;
					
					// Report that a direction has been chosen when the finger is lifted.
				case TouchPhase.Ended:

					if(Time.time - timeOfTouch>0.1f){
						directionChosen = true;

					}else{
						directionChosen = false;

					}

					break;
				}
			}


//move

			if (Input.touchCount == 1  && Input.GetTouch(0).phase != TouchPhase.Moved && directionChosen == false) {


				Ray ray3 = Camera.main.ScreenPointToRay (Input.touches [0].position);



				RaycastHit hit3;
			

				if(Physics.Raycast(ray3, out hit3, Mathf.Infinity)&& hit3.collider.tag=="FLOOR"){

					Vector3 target = new Vector3(hit3.point.x, 0 , hit3.point.z);

				
					
					clickendpoint = hit3.point;
					
					
					movePlayerClick(hit3.point);
					
					playermoving = true;
					
					tr.LookAt(hit3.point); 



					//tr.transform.position =  Vector3.Lerp (tr.transform.position, hit3.point, Time.deltaTime * 2.0f);

					myxpos	=hit3.point.x; //Input.touches [0].position.x;
					myypos	=hit3.point.z;  //Input.touches [0].position.y;



				}
			}
		
		
		}


#else


		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Debug.DrawRay (ray.origin, ray.direction*100.0f, Color.green);

		RaycastHit hitman ;


		if(Input.GetMouseButtonDown(0)){

		if(Physics.Raycast(ray, out hitman, Mathf.Infinity)&&  hitman.collider.tag=="FLOOR"){



				//float speed = Vector3.Distance(tr.position, );

				//tr.position =   Vector3.Lerp (tr.position, hitman.point, Time.deltaTime * 5.0f); // hitman.point*movSpeed * Time.deltaTime;

				 // Vector3.MoveTowards(tr.transform.position, hitman.point, 1.0f * Time.deltaTime);


				myxpos	=hitman.point.x; //Input.touches [0].position.x;
				myypos	=hitman.point.y;  //Input.touches [0].position.y;
				myzpos	=hitman.point.z;



				clickendpoint = hitman.point;

			
				movePlayerClick(hitman.point);

				playermoving = true;

					tr.LookAt(hitman.point); 
			            

				
			} ///raycasr



		}//mousedown






				if (ClientID == gameObject.name) {//움직이는게 나일때
					h = Input.GetAxis ("Horizontal");
					v = Input.GetAxis ("Vertical");
					
					tr.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);
					movDir = (tr.forward * v) + (tr.right * h);
					movDir.y -= 20f * Time.deltaTime;
					
					_controller.Move (movDir * movSpeed * Time.deltaTime);//일단 내걸 움직인다.
					
					if(!isSame (tr,pre_tr)){//내 위치에 변화가 생길경우( 랙가능성 )
						data = ClientID+":"+tr.position.x+","+tr.position.y+","+tr.position.z;
						SocketStarter.Socket.Emit("movePlayerREQ",data);//내위치를 서버에 알린다.
						pre_tr = t2v(tr);
					}
				}// end if keyboard move




#endif


//ifmove
		if(playermoving && clickendpoint != tr.position){
			
			movePlayerClick(clickendpoint);
			
		}
		
		if(clickendpoint == tr.position){
			
			playermoving =false;
		}





	}//end update


	void movePlayerClick (Vector3 clickplace){
		
		float step = 5 * Time.deltaTime;
		tr.position = Vector3.MoveTowards(tr.position, clickplace, step);
		
		
	}

	void OnGUI(){

	
		if(GUI.Button (new Rect(0,30,90,50),"x: " + myxpos)){
			//Application.LoadLevel("scLogIn");
		}
	

		if(GUI.Button (new Rect(0,70,90,50),"y: " + myypos)){
			//Application.LoadLevel("scLogIn");
		}

		if(GUI.Button (new Rect(0,90,90,50),"z: " +myzpos)){
			//Application.LoadLevel("scLogIn");
		}


		if(GUI.Button (new Rect(50,100,90,50),"touch: " +directionChosen  +":"+timeOfTouch)){
			//Application.LoadLevel("scLogIn");
		}
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
}
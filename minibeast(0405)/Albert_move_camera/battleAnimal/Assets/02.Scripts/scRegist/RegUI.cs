using UnityEngine;
using System.Collections;

public class RegUI : MonoBehaviour {
	public string id,password,repassword,username;
	
	float lw,lh,lx,ly;
	float tw,th,tx,ty;
	float bw,bh,bx,by;
	int size;
	private DBManager _dbManager;
	// Use this for initialization
	void Start () {
		lw = 100;lh = 50; lx = 10;ly = 10;
		tw = 200;th = 50;tx = 110;ty = 10;
		bw = 100;bh = 50;bx = 110;by = 250;
		
		size = 20;
		
		_dbManager = GetComponent<DBManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		GUI.Label (new Rect(lx,ly,lw,lh),"<size="+size+">Email :</size>");
		GUI.Label (new Rect(lx,ly+(lh+10),lw,lh),"<size="+size+">Password :</size>");
		GUI.Label (new Rect(lx,ly+2*(lh+10),lw,lh),"<size="+size+">Password :</size>");
		GUI.Label (new Rect(lx,3*(lh+10),lw,lh),"<size="+size+">User Name :</size>");
		
		id = GUI.TextArea (new Rect(tx,ty,tw,th),id,25);
		password = GUI.PasswordField (new Rect(tx,ty+(th+10),tw,th),password,"*"[0],25);
		repassword = GUI.PasswordField (new Rect(tx,ty+2*(th+10),tw,th),repassword,"*"[0],25);
		username = GUI.TextArea (new Rect(tx,ty+3*(th+10),tw,th),username,25);
		
		if (GUI.Button (new Rect (bx,by,bw,bh), "Register")) {
			StartCoroutine (RegLoginData(id, password,repassword, username ));	
		}	
		
		if (GUI.Button (new Rect (bx+bw+10,by,bw,bh), "Cancel")) {
			Application.LoadLevel ("scPreStart");
		}
	}

	private IEnumerator RegLoginData (string email, string password, string password2, string username)
	{
		
		yield return StartCoroutine (_dbManager.RegUser(email, password,password2, username ));
		
		string emailman = _dbManager.fuckdata.GetString ("password");
		
		
		Debug.Log("emailman : "+ _dbManager.fuckdata.GetString("email")) ;
		
		username = _dbManager.fuckdata.GetString("username");
		
		if(username !=""){			
			Application.LoadLevel ("scPreStart");
			Debug.Log("loggedin fucker : "+_dbManager.fuckdata.GetString("email")) ;
			
		}else{
			
			Debug.Log("not logged in : "+_dbManager.fuckdata.GetString("email")) ;
		}	
		
		yield return null;	
		
	}
	
	

}
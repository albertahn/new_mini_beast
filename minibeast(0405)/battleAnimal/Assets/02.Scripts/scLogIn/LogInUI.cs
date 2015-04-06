using UnityEngine;
using System.Collections;

public class LogInUI : MonoBehaviour {
	public string id,password,username;

	float lw,lh,lx,ly;
	float tw,th,tx,ty;
	float bw,bh,bx,by;
	int size;
	private DBManager _dbManager;
	// Use this for initialization
	void Start () {
		lw = 100;lh = 50; lx = 10;ly = 10;
		tw = 100;th = 50;tx = 110;ty = 10;
		bw = 50;bh = 50;bx = 110;by = 130;
	
		size = 20;

		_dbManager = GetComponent<DBManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.Label (new Rect(lx,ly,lw,lh),"<size="+size+">ID :</size>");
		GUI.Label (new Rect(lx,ly+(lh+10),lw,lh),"<size="+size+">Password :</size>");

		id = GUI.TextArea (new Rect(tx,ty,tw,th),id,25);
		password = GUI.PasswordField (new Rect(tx,ty+(th+10),tw,th),password,"*"[0],25);

		if (GUI.Button (new Rect (bx,by,bw,bh), "Log-In")) {
			StartCoroutine (GetLoginData(id,password));			
		}	

		if (GUI.Button (new Rect (bx+bw+10,by,bw,bh), "Cancel")) {
			Application.LoadLevel ("scPreStart");
		}
	}

	
	private IEnumerator GetLoginData (string email, string password)
	{		
		yield return StartCoroutine (_dbManager.LoginUser(id, password));
		
		string emailman = _dbManager.fuckdata.GetString ("password");
		
		//Debug.Log("mailman:  "+ password);		
		// LoginServer.hello ();
		//if (_server.data.ContainsKey ("character")) {		
		//Debug.Log("emailman : "+ _dbManager.fuckdata.GetString("email")) ;
		
		username = _dbManager.fuckdata.GetString("username");
		
		if(username !=""){			
			Debug.Log("loggedin fucker : "+_dbManager.fuckdata.GetString("email")) ;
			
			PlayerPrefs.SetString("email", _dbManager.fuckdata.GetString("email"));
			
			PlayerPrefs.SetString("username", _dbManager.fuckdata.GetString("username"));
			
			PlayerPrefs.SetString("user_index", _dbManager.fuckdata.GetString("index"));
			Application.LoadLevel ("scStart");
			
		}else{			
			Debug.Log("not logged in : ") ;
		}		
		
		yield return null;		
	}
}
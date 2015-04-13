using UnityEngine;
using System.Collections;

public class startUI : MonoBehaviour {
	private float bx,by,bw,bh;

	// Use this for initialization
	void Start () {
		bx = 10;by = 10;
		bw = 100;bh = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (GUI.Button (new Rect (bx, by, bw, bh), "Multy")) {
			Application.LoadLevel("scWait");
		}
		if(GUI.Button (new Rect(bx,by+bw+10,bw,bh),"Logout")){
			
			PlayerPrefs.SetString("email", "");
			
			PlayerPrefs.SetString("username", "");
			
			PlayerPrefs.SetString("user_index", "");
			Application.LoadLevel("scPreStart");
		}
		if(GUI.Button (new Rect(bx,by+2*(bw+10),bw,bh),"Exit")){
			Application.Quit();
		}
	}
}

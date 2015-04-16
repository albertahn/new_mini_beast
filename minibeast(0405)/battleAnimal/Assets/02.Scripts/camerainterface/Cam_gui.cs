using UnityEngine;
using System.Collections;

public class Cam_gui : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution(480, 800, true);

	}


	void OnGUI(){
		
				if (GUI.Button (new Rect (300, 0, 50, 30), "logout")) {

							PlayerPrefs.SetString("email", "");
							PlayerPrefs.SetString("username", "");
							PlayerPrefs.SetString("user_index", "");



			Application.LoadLevel ("scLogin");
			
				}
		}
	
	// Update is called once per frame
	void Update () {
	
	}
}

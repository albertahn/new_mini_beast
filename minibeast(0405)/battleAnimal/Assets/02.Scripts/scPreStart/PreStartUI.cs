using UnityEngine;
using System.Collections;

public class PreStartUI : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("email")!=""){
			Application.LoadLevel("scStart");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void scLogin()
	{
		Application.LoadLevel("scLogIn");
	}
	
	public void scRegister()
	{
		Application.LoadLevel("scRegist");
	}
	
	public void Exit()
	{
		Application.Quit ();
	}
	
}

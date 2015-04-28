using UnityEngine;
using System.Collections;

public class startUI : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Multy()
	{
		Application.LoadLevel("scWait");
	}
	
	public void Logout()
	{
		PlayerPrefs.SetString("email", "");
		
		PlayerPrefs.SetString("username", "");
		
		PlayerPrefs.SetString("user_index", "");
		Application.LoadLevel("scLogIn");
	}
	
	public void Exit()
	{
		Application.Quit();
	}
	
}

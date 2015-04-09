using UnityEngine;
using System.Collections;

public class Level_up : MonoBehaviour {

	public int currentlevel;

	public int killint;

	// Use this for initialization
	void Start () {


		}
	
	// Update is called once per frame
	void Update () {





		//int i = PlayerPrefs.GetInt ("minions_killed");


		/*switch (PlayerPrefs.GetInt ("minions_killed")) {
				case 0:
				case 1:
				case 2:
						{
								//Lv0
								currentlevel= 0;
				
						}
				case 3:
				case 4:
				case 5:
						{
								currentlevel = 1;

			//StartEvolution();


						}
				
	
				}//end switch*/
		}


	void OnGUI(){
		
		if (GUI.Button (new Rect (400, 0, 70, 30), "kill: "+ PlayerPrefs.GetInt("minions_killed"))) {
			
		
		}


		if (GUI.Button (new Rect (470, 0, 70, 30), "Level: "+ currentlevel)) {
			
			
		}
	}


	void StartEvolution(){


		Debug.Log ("evelution");

	}//end evolution

}

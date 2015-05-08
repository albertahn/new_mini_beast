using UnityEngine;
using System.Collections;

public class UI_skill_manager : MonoBehaviour {

	public GameObject myplayer;


//game character skill scripts


	public Component myskillscript;

	public bool firstOn, secondOn, thirdOn;


	// Use this for initialization
	void Start () {

		firstOn = false;
		secondOn= false;
		thirdOn= false;

		//myskillscript = GameObject.Find("MultiManager").GetComponent<LobbyUI>();
		//myskillscript = myplayer.GetComponent<DogSkill_GUI>();
	}
	
	// Update is called once per frame
	void Update () {

		if (myplayer != null) {

						myplayer = GameObject.Find (ClientState.id);
		
				} else {
				

            //depending on my character change myskillscript

		switch (ClientState.character)
		{
		case "dog":

				DogSkill_GUI dog_skill_gui  = GameObject.Find (ClientState.id).GetComponent<DogSkill_GUI>();

				if(firstOn){

							dog_skill_gui.Skill1_bot(); 
						    firstOn =false;

				}else if(secondOn){
					dog_skill_gui.Skill2_bot(); 
					secondOn =false;

				}else if(thirdOn){

					dog_skill_gui.Skill3_bot(); 
					thirdOn =false;

				}//endif


		
			break;
		case "turtle":

				TurtleSkill turtleskill = GameObject.Find (ClientState.id).GetComponent<TurtleSkill>();

				if(firstOn){
					
					//turtleskill.Skill1_bot(); 
					firstOn =false;
					
				}//endif

		
			break;
		case "other":
			


			break;
		}//switch

		}//end esle
	}//end update


//first skill
	public void firstSkill(){

		firstOn = true;


	}

	public  void secondSkill(){

		secondOn = true;

	}

	public void thirdSkill(){

		thirdOn = true;

		Debug.Log ("third on");

	}
}

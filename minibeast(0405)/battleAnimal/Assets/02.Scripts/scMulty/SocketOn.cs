using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//An element with the same key already exists in the dictionary에러를 피하려면 Socket.on을 모아놔야 한다.
public class SocketOn : MonoBehaviour {
	private SpawnPlayer _spawnPlayer;
	private SpawnMinion _spawnMinion;
	private MoveCtrl _moveCtrl;
	private LobbyUI _lobbyUI;

	public string ClientID;
	private string addId;
	
	public string resID;
	public Vector3 newPos;
	public Vector3 attackPos;
	private bool moveUserSwitch;

	private string outID;
	private bool outUserSwitch;
		
	private string attackID;
	private bool attackSwitch;
	private string attackTarget;
	private bool moveSyncSwitch;
	private bool loadlevelSwitch;

	private bool building_health_change;
	private string building_name;
	private int building_hp_int;


	private bool minion_health_change;
	private string minion_name;
	private int minion_hp_int;

	private string minionID;
	private Vector3 minionPos, minionTg;
	private bool minionSyncSwitch;
	// Use this for initialization

	public GameObject nmanager; // = GameObject.Find("NetworkManager");
	public Skill_socket_reciever skill_reciever;

	private minionAttackReceiver _MAReceiver;

	void Start () {
		_MAReceiver = GetComponent<minionAttackReceiver>();

		skill_reciever = GameObject.Find("NetworkManager").GetComponent<Skill_socket_reciever> ();
		//skill_reciever = nmanager.GetComponent<Skill_socket_reciever> ();

		Screen.SetResolution(480, 800, true);

		_spawnPlayer = GetComponent<SpawnPlayer> ();
		_spawnMinion = GetComponent<SpawnMinion> ();
		_lobbyUI = GameObject.Find("MultiManager").GetComponent<LobbyUI>();
		ClientID = ClientState.id;
		
		moveUserSwitch=false;
		outUserSwitch = false;
		attackSwitch = false;
		moveSyncSwitch = false;
		minionSyncSwitch = false;

		SocketStarter.Socket.On ("createRoomRES", (data) =>{
			string temp = data.Json.args[0].ToString();
			if(temp==ClientID){		
				loadlevelSwitch=true;
			}
		});

		SocketStarter.Socket.On ("youMaster", (data) =>{
			Debug.Log("i'am Master");//edit
			ClientState.isMaster = true;
		});
	
		SocketStarter.Socket.On("createPlayerRES",(data) =>
		{//접속한 플레이어가 있을때 호출된다.
			string temp = data.Json.args[0].ToString();
			string[] temp2;
			string[] pos;
			Vector3 spawnPos;
			string _char;
			string team;
			
			temp2 = temp.Split(':');
			addId = temp2[0];//접속한 유저의 아이디
			pos = temp2[1].Split(',');
			_char = temp2[2];
			team = temp2[3];
			
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));

			while(_spawnPlayer.spawnSwitch==true){
			}
			_spawnPlayer.setSpawn(addId,spawnPos,_char,team);//해당 user를 instantiate한다.

			if(ClientID==addId){
				while(true){
					if(_spawnPlayer.spawnSwitch==false)
						break;
				}
				SocketStarter.Socket.Emit ("preuserREQ", addId);
			}
		});

		SocketStarter.Socket.On("createRedMinionRES",(data) =>
		{
			string temp = data.Json.args[0].ToString();
			string[] pos;
			Vector3 spawnPos;
			string id;
			
			pos = temp.Split(':');
			id = pos[0];//접속한 유저의 아이디
			pos = pos[1].Split(',');
			
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));

			while(_spawnMinion.REDspawnSwitch==true){
			}
			_spawnMinion.REDsetSpawn(id,spawnPos);//해당 user를 instantiate한다.
		});

		SocketStarter.Socket.On("createBlueMinionRES",(data) =>
		                        {
			string temp = data.Json.args[0].ToString();
			string[] pos;
			Vector3 spawnPos;
			string id;
			
			pos = temp.Split(':');
			id = pos[0];//접속한 유저의 아이디
			pos = pos[1].Split(',');
			
			spawnPos = new Vector3(float.Parse(pos[0]),
			                       float.Parse(pos[1]),
			                       float.Parse(pos[2]));
			
			while(_spawnMinion.BLUEspawnSwitch==true){
			}
			_spawnMinion.BLUEsetSpawn(id,spawnPos);//해당 user를 instantiate한다.
		});

		SocketStarter.Socket.On ("preuser1RES", (data) => {			
			string temp = data.Json.args[0].ToString();
			string[] list;
			string sender;
			string[] pos;
			string id;
			string[] temp3;
			Vector3 spawnPos;
			
			string[] temp2 = temp.Split('=');
			sender = temp2[0];
			list = temp2[1].Split('_');
			if(ClientID==sender){
				for(int i=0;i<list.Length-2;i++)
				{
					temp3 = list[i].Split(':');
					id =temp3[0];
					pos = temp3[1].Split(',');
					spawnPos = new Vector3(float.Parse(pos[0]),
					                       float.Parse(pos[1]),
					                       float.Parse(pos[2]));
					if(id[0]=='r'){
						while(_spawnMinion.REDspawnSwitch==true){
						}
						_spawnMinion.REDsetSpawn(id,spawnPos);
					}else if(id[0]=='b'){
						while(_spawnMinion.BLUEspawnSwitch==true){
						}
						_spawnMinion.BLUEsetSpawn(id,spawnPos);
					}
				}
			}
		});

		SocketStarter.Socket.On ("preuser2RES", (data) => {			
			string temp = data.Json.args[0].ToString();
			string[] list;
			string sender;
			string[] pos;
			string id;
			string[] temp3;
			Vector3 spawnPos;
			string _char;
			string team;

			string[] temp2 = temp.Split('=');
			sender = temp2[0];
			list = temp2[1].Split('_');
			if(ClientID==sender){
				for(int i=0;i<list.Length-2;i++)
				{
					temp3 = list[i].Split(':');
					id =temp3[0];
					pos = temp3[1].Split(',');
					spawnPos = new Vector3(float.Parse(pos[0]),
					                       float.Parse(pos[1]),
					                       float.Parse(pos[2]));
					_char = temp3[2];
					team = temp3[3];
					while(_spawnPlayer.spawnSwitch==true){
					}
						_spawnPlayer.setSpawn(id,spawnPos,_char,team);
				}
			}
		});

		SocketStarter.Socket.On ("movePlayerRES", (data) =>
		{
			string[] temp = data.Json.args[0].ToString().Split(':');
			resID = temp[0];
			string[] resPos = temp[1].Split(',');
			newPos = new Vector3(float.Parse(resPos[0]),
			                     float.Parse(resPos[1]),
			                     float.Parse(resPos[2]));
			if(ClientID!=resID){
				moveUserSwitch=true;
			}
		});

		SocketStarter.Socket.On ("moveSyncRES", (data) =>
		{
			string[] temp = data.Json.args[0].ToString().Split(':');
			resID = temp[0];
			string[] resPos = temp[1].Split(',');
			newPos = new Vector3(float.Parse(resPos[0]),
			                     float.Parse(resPos[1]),
			                     float.Parse(resPos[2]));
			if(ClientID!=resID){
				moveSyncSwitch=true;
			}
		});

		SocketStarter.Socket.On ("minionSyncRES", (data) =>
		                         {
			if(ClientState.isMaster==false){
				string[] temp = data.Json.args[0].ToString().Split('|');
				
				for(int i=0;i<temp.Length-1;i++){//edit
					string[] temp2 = temp[i].Split(':');
					minionID = temp2[0];
					string[] resPos = temp2[1].Split(',');				
					minionPos = new Vector3(float.Parse(resPos[0]),
					                        float.Parse(resPos[1]),
					                        float.Parse(resPos[2]));
					resPos = temp2[2].Split(',');
					minionTg = new Vector3(float.Parse(resPos[0]),
					                       float.Parse(resPos[1]),
					                       float.Parse(resPos[2]));

					while(minionSyncSwitch){

					}
					minionSyncSwitch=true;
				}
			}
		});

		SocketStarter.Socket.On ("minionAttackRES", (data) =>
		{
			if(ClientState.isMaster==false){
				_MAReceiver.receive(data.Json.args[0].ToString());
			}
		});

		SocketStarter.Socket.On ("attackRES", (data) =>
		{
			string[] temp = data.Json.args[0].ToString().Split(':');
			attackID = temp[0];
			attackTarget = temp[1];

			attackSwitch=true;
		});

		SocketStarter.Socket.On ("imoutRES", (data) =>{			
			string temp = data.Json.args[0].ToString();
			outID = temp;

			outUserSwitch=true;
		});


		SocketStarter.Socket.On ("attackMinion", (data) =>{
			
			string[] temp = data.Json.args[0].ToString().Split(':');
			minion_name = temp[0];
			minion_hp_int = int.Parse(temp[1]);
			
			//Debug.Log("attack: " + minion_name+":"+minion_hp_int);
			
			minion_health_change= true;
		});



		//building attack
		SocketStarter.Socket.On ("attackBuilding", (data) =>{	
			
			
			string[] temp = data.Json.args[0].ToString().Split(':');
			building_name = temp[0];
			building_hp_int = int.Parse(temp[1]);
			
			
			//Debug.Log("attack: " + building_name+":"+building_hp_int);
			
			building_health_change= true;
		});
		
		SocketStarter.Socket.Emit ("createRoomREQ", ClientID+":"+ClientState.room);
//skills sync

	
		//skill attack
		SocketStarter.Socket.On ("SkillAttack", (data) =>{
			skill_reciever.skillShot(data.Json.args[0].ToString());
		});




	}//end start
	
	// Update is called once per frame
	void Update () {
		if (moveUserSwitch) {
			moveUser();
			moveUserSwitch=false;
		}

		if (moveSyncSwitch) {
			moveSync();
			//moveSyncSwitch=false;
		}

		if (attackSwitch) {
			GameObject.Find(attackID).GetComponent<MoveCtrl>().attack(attackTarget);
			attackSwitch=false;
		}

		if (outUserSwitch) {
			GameObject a = GameObject.Find(outID);
			GameObject.Destroy(a);
			outUserSwitch=false;
		}

		if (loadlevelSwitch) {
			_lobbyUI.isUI =false;
			StartCoroutine(_spawnPlayer.CreatePlayer());
			loadlevelSwitch=false;
		}

		if (minionSyncSwitch) {
			GameObject a = GameObject.Find (minionID);
			if(a!=null){
				if(a.name[0]=='r')
				a.GetComponent<minionCtrl>().setSync(minionPos,minionTg);
				else
					a.GetComponent<blueMinionCtrl>().setSync(minionPos,minionTg);
			}
			minionSyncSwitch=false;
		}
	}

//building health change
	void change_building_health(){
		GameObject buildingnow = GameObject.Find (building_name);
		buildingnow.GetComponent<MainFortress>().hp = building_hp_int;
		
	}
	//change minion health
	void change_minion_health(){
		GameObject mininow = GameObject.Find (""+minion_name);
		mininow.GetComponent<minion_state>().hp = minion_hp_int;
		
	}

	void moveUser(){
		GameObject a = GameObject.Find (resID);
		a.GetComponent<MoveCtrl>().clickendpoint = newPos;
		a.GetComponent<MoveCtrl>().move ();
	}

	void moveSync(){
		if (ClientID != resID) {
				GameObject a = GameObject.Find (resID);
				//a.GetComponent<Transform> ().position = newPos;
		
				float step = 4 * Time.deltaTime;
				a.transform.position = Vector3.MoveTowards (a.transform.position, newPos, step);
				a.transform.LookAt (newPos);
		
				if (a.GetComponent<Transform> ().position == newPos) {
					moveSyncSwitch = false;
				}// arrived switch
		}
	}
}
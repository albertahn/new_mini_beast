﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class waitSocketOn : MonoBehaviour {
	private string clientID;
	private	waitGUI _waitGUI;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(480, 800, true);

		clientID = ClientState.id;
		_waitGUI = GetComponent<waitGUI> ();

		waitSocketStarter.Socket.On("createPlayerRES",(data) =>
		                        {//접속한 플레이어가 있을때 호출된다.
			string[] temp = data.Json.args[0].ToString().Split(':');
			int num = int.Parse (temp[0]);
			string id = temp[1];//접속한 유저 아이디

			if(clientID == id){
				ClientState.order = num;
				waitSocketStarter.Socket.Emit ("preuserREQ", id);
			}else{
				_waitGUI.remoteAddUser(num,id);
			}
		});

		waitSocketStarter.Socket.On ("imoutRES", (data) =>{
			string temp = data.Json.args[0].ToString();	
			int a = int.Parse(temp);
			_waitGUI.remoteDeleteUser(a);
		});

		waitSocketStarter.Socket.On("preuserRES",(data) =>
		                            {//접속한 플레이어가 있을때 호출된다.
			string temp = data.Json.args[0].ToString();
			string[] temp2 = temp.Split('=');
			string sender = temp2[0];

			if(clientID==sender){
				string[] temp3 = temp2[1].Split('-');
				for(int i=0;i<temp3.Length-1;i++){
					string[] temp4 = temp3[i].Split(':');
					int num = int.Parse(temp4[0]+"");
					string id = temp4[1];
					string character = temp4[2];
					_waitGUI.remoteAddUser(num,id);
					_waitGUI.remoteSetCharacter(num,character);
				}
			}
		});

		waitSocketStarter.Socket.On ("characterSelectRES", (data) =>{
			string temp = data.Json.args[0].ToString();
			string[] temp2 = temp.Split(':');
			int order = int.Parse(temp2[0]+"");
			string character = temp2[1];

			_waitGUI.remoteSetCharacter(order,character);
		});

		waitSocketStarter.Socket.On ("createRoomRES", (data) =>{
			string temp = data.Json.args[0].ToString();
			ClientState.room = temp;
			waitSocketStarter.Socket.Emit ("createPlayerREQ", ClientState.id);
		});


		//waitSocketStarter.Socket.Emit ("createRoomREQ", clientID);

		waitSocketStarter.Socket.Emit ("joinRoomREQ", ClientState.room);

	}//starts
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
	}
}

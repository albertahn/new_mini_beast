﻿using UnityEngine;
using System.Collections;

public class playerAttackReceiver : MonoBehaviour {
	private bool switch_;
	private string attacker;
	private string target;
	private MoveCtrl _moveCtrl;
	private tutu_MoveCtrl _tutu_moveCtrl;
	private string character;
	
	// Use this for initialization
	void Start () {
		switch_ = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(switch_){
			GameObject a = GameObject.Find (attacker);
			
			if (a != null) {
				if(character=="dog"){
					_moveCtrl = a.GetComponent<MoveCtrl>();
					_moveCtrl.attack(target);
				}
				else if(character=="turtle"){
					_tutu_moveCtrl = a.GetComponent<tutu_MoveCtrl>();
					_tutu_moveCtrl.attack(target);
				}
			}
			
			switch_=false;
		}	
	}
	public void receive(string data){
		string[] temp = data.Split (':');
		while (switch_) {}
		attacker = temp [0];
		character = temp [1];
		target = temp [2];
		Debug.Log("attacker = "+attacker+" target = "+target);
		switch_ = true;
	}
}

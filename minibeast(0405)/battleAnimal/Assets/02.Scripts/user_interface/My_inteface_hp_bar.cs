using UnityEngine;
using System.Collections;

public class My_inteface_hp_bar : MonoBehaviour {
	public GameObject player, hpText;
	private int maxHP;
	// Use this for initialization
	void Start () {

		//player = GameObject.Find (PlayerPrefs.GetString("email"));

		//maxHP = player.GetComponent<PlayerHealthState> ().hp;



	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find (PlayerPrefs.GetString("email"));
		
		maxHP = player.GetComponent<PlayerHealthState> ().hp;
		int hp = player.GetComponent<PlayerHealthState> ().hp;
		Vector3 temp = new Vector3 ((float)hp / maxHP, 1, 1);
		this.transform.localScale = temp;
		
		hpText.GetComponent<TextMesh>().text = ""+hp.ToString();
	}
}

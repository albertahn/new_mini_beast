using UnityEngine;
using System.Collections;

public class SkillFirstCrl : MonoBehaviour {

	public int damage;
	private float speed;
	public float birth;
	private float durationTime;
	
	// Use this for initialization
	void Start () {
		damage = 120;
		speed = 1000.0f;
		rigidbody.AddForce (transform.forward * speed);
		birth = Time.time;
		durationTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - birth) > durationTime)
			Destroy (this.gameObject);	
	}

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "MINION") {

			Debug.Log("skill first hit min");

			coll.gameObject.GetComponent<minion_state>().Heated(gameObject);
			Destroy (this.gameObject);

				}

	}


}

using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {
	private int damage;
	private float speed;
	public float birth;
	private float durationTime;

	// Use this for initialization
	void Start () {
		damage = 20;
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
		Destroy (this.gameObject);
	}
}

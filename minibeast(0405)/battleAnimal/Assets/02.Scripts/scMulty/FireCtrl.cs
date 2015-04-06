using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {

	public GameObject bullet;
	public Transform firePos;
	public MeshRenderer _renderer;

	private float birth;
	private float duration;

	public float distance;


	// Use this for initialization
	void Start () {
		_renderer.enabled = false;	
		duration = 0.5f;
		distance = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Fire(){
		if ((Time.time - birth) > duration) {
						StartCoroutine (this.CreateBullet ());
						StartCoroutine (this.ShowMuzzleFlash ());
			birth = Time.time;
		}
	}

	IEnumerator CreateBullet(){
		Instantiate(bullet,firePos.position,firePos.rotation);
		yield return null;
	}

	IEnumerator ShowMuzzleFlash(){
		_renderer.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		_renderer.enabled = false;
	}
}
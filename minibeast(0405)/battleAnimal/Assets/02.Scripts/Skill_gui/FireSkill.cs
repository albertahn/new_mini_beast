using UnityEngine;
using System.Collections;

public class FireSkill : MonoBehaviour {

	public GameObject bulleta;
	public Transform firePosa;
	public MeshRenderer _renderera;
	
	private float birth;
	private float duration;
	
	public float distancea;
	
	
	// Use this for initialization
	void Start () {
		_renderera.enabled = false;	
		duration = 0.5f;
		distancea = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Fireman(){
		if ((Time.time - birth) > duration) {
			
			StartCoroutine (this.CreateBullet ());
			StartCoroutine (this.ShowMuzzleFlash ());
			birth = Time.time;

		}
	}
	
	IEnumerator CreateBullet(){

		Instantiate(bulleta,firePosa.position,firePosa.rotation);
		yield return null;
	}
	
	IEnumerator ShowMuzzleFlash(){
		_renderera.enabled = true;
		yield return new WaitForSeconds(Random.Range(0.01f,0.2f));
		_renderera.enabled = false;
	}
}
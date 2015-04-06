using UnityEngine;
using System.Collections;

public class animationCtrl : MonoBehaviour {
	private MoveCtrl _moveCtrl;

	[System.Serializable]
	public class Anim{
		public AnimationClip idle;
		public AnimationClip run;
	}

	public Anim anim;

	public Animation _animation;

	// Use this for initialization
	void Start () {
		_animation = GetComponentInChildren<Animation> ();
		_animation.clip = anim.idle;
		_animation.Play ();
		_moveCtrl = GetComponent<MoveCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_moveCtrl.h>=0.1f) {
			_animation.CrossFade(anim.run.name,0.3f);
		}else if(_moveCtrl.h<=-0.1f){

		}else if (_moveCtrl.v>=0.1f) {
			_animation.CrossFade(anim.run.name,0.3f);
		}else if(_moveCtrl.v<=-0.1f){
			_animation.CrossFade(anim.run.name,0.3f);
		}else {
			_animation.CrossFade(anim.idle.name,0.3f);
		}
	}
}

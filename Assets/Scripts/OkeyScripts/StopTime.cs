using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTime: MonoBehaviour {

	private TweenTransforms _ImTransf;
	private bool isMoving=true;
	private Transform _transform;
	private IEnumerator coroutine;
	private IEnumerator coroutine1;
	private bool usable = true;

	public bool blocked;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform>();
		_ImTransf = GetComponent<TweenTransforms> ();
	}

	// Update is called once per frame
	void Update () {

		//StopTime
		if(Input.GetKeyDown(KeyCode.K)){
			if (usable){
				if(isMoving && !blocked){
					coroutine1 = Cooldown ();
					StartCoroutine (coroutine1);
					coroutine = StoppingTime ();
					StartCoroutine (coroutine);
				}
			}
			usable = false;
		}

	}

	private IEnumerator StoppingTime(){
		SoundManager.Instance.StopTimeBGM();
		_ImTransf.Pause ();
		isMoving= false;
		yield return new WaitForSeconds (5.0f);
		SoundManager.Instance.StopTimeEnd();
		_ImTransf.Resume ();
		isMoving = true;
	}

	private IEnumerator Cooldown(){
		yield return new WaitForSeconds (6.0f);
		usable = true;
	}
}

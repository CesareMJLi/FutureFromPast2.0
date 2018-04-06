using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatf: MonoBehaviour {

	private Transform _transform;

	void Start(){
		_transform = GetComponent<Transform>();
		
	}

	void OnTriggerStay2D(Collider2D coll){

			coll.transform.parent = _transform;

		
	}
	void OnTriggerExit2D(Collider2D coll){
			
		coll.transform.parent = null;
	}

}

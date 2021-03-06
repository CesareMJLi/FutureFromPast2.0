using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour
{
	private Transform _transform;
	private TweenTransforms _ImTransf;
	public GameObject _obstacle;
	private string _tag;
	public GameObject _other;
	private Transform  _transformOther;
	private bool PlayerTouch=false;
	private Rigidbody2D _rigidbody;

	private Vector3 _initialScale;
	private Vector3 _initialPosition;
	private Quaternion _initialRotation;
	private bool _migrated;

	private bool isMoving=true;
	private IEnumerator coroutine;
	private IEnumerator coroutine1;
	private bool usable = true;

	public static bool obRotateBlocked;


	// Use this for initialization
	void Start ()
	{	
		
		_transform = GetComponent<Transform>();
		_transformOther = _other.GetComponent<Transform> ();
		_tag = _obstacle.tag;


		if (_tag.Equals ("ObRotatingFuture")) {
			 _ImTransf = GetComponent<TweenTransforms> ();

			 _ImTransf.startingVector = new Vector3 (0, 0, _transform.eulerAngles.z);
			 _ImTransf.endVector = new Vector3 (0, 0,  - 360 + _ImTransf.startingVector.z);
			 _ImTransf.Begin ();
		}

		if (_tag.Equals ("ObRotatingPast")) {
			_ImTransf = GetComponent<TweenTransforms> ();

			_ImTransf.startingVector = new Vector3 (0, 0, _transform.eulerAngles.z);
			_ImTransf.endVector = new Vector3 (0, 0,  - 360 + _ImTransf.startingVector.z);
			_ImTransf.Begin ();
		}

		if (_tag.Equals ("ObMigratable")) {
			_rigidbody = GetComponent<Rigidbody2D> ();
			_migrated = false;
		}

		if (_tag.Equals ("ObFalling")) {
			_ImTransf = GetComponent<TweenTransforms> ();
			_ImTransf.startingVector = new Vector3 (_transform.position.x, _transform.position.y, _transform.position.z);
			_ImTransf.endVector = new Vector3 (_ImTransf.startingVector.x, _ImTransf.startingVector.y + 6.0f, _ImTransf.startingVector.z);
		}

	}

	void Awake(){
		_transform = GetComponent<Transform> ();
		_initialPosition = _transform.position;
		_initialRotation = _transform.rotation;
		_initialScale = _transform.localScale;

	}
	
	// Update is called once per frame
	void Update ()
	{	

		if (_tag.Equals ("ObMigratable")) {
			if (Input.GetKeyDown (KeyCode.L) && PlayerTouch) {
				SoundManager.Instance.SwitchObj();
				if (!_migrated) {
					_transform.position = new Vector3 (_transformOther.position.x, -_transformOther.position.y, _transformOther.position.z);
					_transform.Rotate(new Vector3 (0,0, -_transformOther.eulerAngles.z));
					_rigidbody.bodyType = RigidbodyType2D.Dynamic;
					_migrated = true;
				} else {
					_transform.position = _initialPosition;
					_transform.rotation = _initialRotation;
					_rigidbody.bodyType = RigidbodyType2D.Static;
					_migrated = false;
				} 
				
			}
		}


	}

	void OnCollisionExit2D(Collision2D coll){
		//When the obstacle rotating is not touching anything it will rotate
		if(_tag.Equals ("ObRotatingFuture") && coll.gameObject.tag!="Player"){
			 if (_ImTransf.isPlaying) {
			 	_ImTransf.Resume ();
			 }
			obRotateBlocked=false;
		}


		if (coll.gameObject.tag == "Player") {
			PlayerTouch = false;
		}


	}

	void OnCollisionEnter2D(Collision2D coll){
		//Block the movement of the obstacle rotating if hits something
		if (_tag.Equals ("ObRotatingFuture") && coll.gameObject.tag=="Box") {
			 if (_ImTransf.isPlaying) {
			 	_ImTransf.Pause ();
			 }
			obRotateBlocked=true;
		}

		if (coll.gameObject.tag == "Player") {
			PlayerTouch = true;
		}

	}
		
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			PlayerTouch = true;
		}

		if (_tag.Equals ("ObRotatingFuture") && coll.gameObject.tag=="Box") {
			if (_ImTransf.isPlaying) {
				_ImTransf.Pause ();
			}
			obRotateBlocked=true;
		}

	}

	void OnTriggerEnter2D (Collider2D coll){
		if (_tag.Equals ("Portal") && coll.gameObject.tag == "Box") {
			_transformOther.position = new Vector3 (_transformOther.position.x, -_transformOther.position.y, _transformOther.position.z);
			SwitchGravity (_other);

		}
	}



	void SwitchGravity (GameObject ob){
		if (ob.GetComponent <Rigidbody2D> ().gravityScale == 0) {
			ob.GetComponent<Rigidbody2D> ().gravityScale = -1;
		} else {
			
			ob.GetComponent<Rigidbody2D>().gravityScale = 0;
		}
	}


}


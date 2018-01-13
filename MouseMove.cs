using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
	public float moveSpeed;
	private Vector3 rot;

	// Use this for initialization
	void Start () {
		moveSpeed = 0.01f;
		rot = new Vector3 (0.0f, Random.Range (-1.0f, 1.0f), 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject circleTime = GameObject.Find ("Image");
		circleTimer getState = circleTime.GetComponent<circleTimer> ();
		float state = getState.state;

		if (state == 1) {
			gameObject.transform.Translate (moveSpeed, 0.0f, 0.0f);
			gameObject.transform.Rotate (rot);
		} else if (state == 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "mouse(Clone)") {
			gameObject.transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "movingRange") {
			gameObject.transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
		}
	}
}

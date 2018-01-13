using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour {
	public GameObject mousePrefab;
	public GameObject tempMouse ;
	public int mouseCounter;

	private int timeCounter;
	private int randomTime;
	private Vector3 randomPos;
	private Quaternion randomRot;

	// Use this for initialization
	void Start () {
		randomTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject circleTime = GameObject.Find ("Image");
		circleTimer getState = circleTime.GetComponent<circleTimer> ();
		float state = getState.state;

		if (state == 1) {
			if (mouseCounter < 8) {
//				timeCounter++;
//				if (timeCounter >= randomTime) {
					//			GameObject thisMouse = new GameObject ();
					//			thisMouse = mousePrefab;
					mouseCounter++;
					randomPos = new Vector3 (Random.Range (-3.0f, 3.0f), gameObject.transform.position.y, Random.Range (-3.0f, 3.0f));
					randomRot = new Quaternion (0.0f, 1.0f, 0.0f, Random.Range (-90.0f, 90.0f));
					tempMouse = Instantiate (mousePrefab, randomPos, randomRot);
//					tempMouse.tag = "myMouse";
					timeCounter = 0;
					randomTime = (int)Random.Range (100.0f, 200.0f);
//				}
			}
		} else if (state == 0) {
//			GameObject[] killEmAll;
//			killEmAll = GameObject.FindGameObjectsWithTag("myMouse");
//			for (int i = 0; i < mouseCounter; i++) {
//				Destroy (tempMouse);
////				if (gameObject.name == "mouse(Clone)") {
//////				other.gameObject.name == "mouse(Clone)";
////					Destroy (gameObject);
////				}
				
//			}
			mouseCounter = 0 ;
		}
			

	}
}

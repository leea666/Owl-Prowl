using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class owl3 : MonoBehaviour {
	public float rotateSpeed;
	public float moveSpeed;
	public float setSpeed;
	public bool isOut;
	public bool isFighting;
	public bool isCarrying;
	public bool gotStolen;
	public int score;

	private Vector3 oriPos;
	private Quaternion oriRot;
	private Color oriColor;
	private Color oriNest;

	GameObject producer;
	GameObject fightPro;
	GameObject scorePad;

	// Use this for initialization
	void Start () {
		rotateSpeed = 4.0f;
		moveSpeed = 0.0f;
		setSpeed = 0.2f;
		oriPos = gameObject.transform.position;
		oriRot = gameObject.transform.rotation;
//		oriColor = GameObject.Find ("owlbody3").GetComponent<Renderer> ().material.color;
		oriColor = GameObject.Find ("nesttop2").GetComponent<Renderer> ().material.color ;
		oriNest = GameObject.Find ("nesttop2").GetComponent<Renderer> ().material.color;
		isFighting = false;
		producer = GameObject.Find ("MouseProducer");
		fightPro = GameObject.Find ("CloudProducer");
		scorePad = GameObject.Find ("score-2");
		isCarrying = false;
		score = 0;
		gotStolen = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		GameObject circleTime = GameObject.Find ("Image");
		circleTimer getState = circleTime.GetComponent<circleTimer> ();
		float state = getState.state;
		circleTimer getResetPos = circleTime.GetComponent<circleTimer> ();
		bool resetPos = getResetPos.resetPos;

		if (state == 1) {
//			if (Input.GetKey (KeyCode.A)) {
//				gameObject.transform.Rotate (new Vector3 (0.0f, -rotateSpeed, 0.0f));
//			} else if (Input.GetKey (KeyCode.D)) {
//				gameObject.transform.Rotate (new Vector3 (0.0f, rotateSpeed, 0.0f));
//			} else if (Input.GetKeyDown (KeyCode.W)) {
//				moveSpeed = setSpeed;
//				if (!isOut) {
//					isOut = true;
//				}
//			}
			if (resetPos = true) {
				gameObject.transform.position = oriPos;
			}

			scorePad.GetComponent<UnityEngine.UI.Text> ().enabled = true;

			GameObject owl1 = GameObject.Find ("owl");
			owl getpressure3 = owl1.GetComponent<owl> ();
			float pressure3 = getpressure3.pressure3;
			owl getPotC = owl1.GetComponent<owl> ();
			float potC = getPotC.potC;
			Vector3 destination = new Vector3 (0, potC, 0);
			gameObject.transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, 
				destination, 
				Time.deltaTime * rotateSpeed);
			//				gameObject.transform.eulerAngles = new Vector3(0, potA, 0);
			if (pressure3 >= 900) {
				if (!isOut) {
					isOut = true;
				}
				moveSpeed = setSpeed;
			}
			Debug.LogError ("Tawny's score: " + score);
			scorePad.GetComponent<UnityEngine.UI.Text> ().text = "score  " + score * 10;


			gameObject.transform.Translate (new Vector3 (-moveSpeed, 0.0f, 0.0f));

			if (isCarrying == true) {
				GameObject.Find ("owlbody3").GetComponent<Renderer> ().material.color = new Color (Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f, 100.0f/255.0f *Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f, 100.0f/255.0f *Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f);
			} else {
				GameObject.Find ("owlbody3").GetComponent<Renderer> ().material.color = new Color (1.0f, 100.0f/255.0f, 100.0f/255.0f) ;
			}
			//		Debug.Log (moveSpeed);
			if (isOut && score > 0) {
				GameObject.Find ("nesttop2").GetComponent<Renderer> ().material.color = new Color (Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f, 0.0f, 0.0f);
			} else {
				GameObject.Find ("nesttop2").GetComponent<Renderer> ().material.color = oriNest;
			}
		} else if (state == 0) {
			rotateSpeed = 1.0f;
			moveSpeed = 0.0f;
			setSpeed = 0.2f;
			gameObject.transform.position = oriPos;
			isFighting = false;
			isCarrying = false;
			gotStolen = false;
		} else if (state == 2) {
			score = 0;
			scorePad.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other);
		if (other.gameObject.name == "nest2") {
			gameObject.transform.rotation = oriRot;
			gameObject.transform.position = oriPos;
			moveSpeed = 0.0f;
			isOut = false;
			if (isCarrying == true) {
				score++;
				isCarrying = false;
			}
			gotStolen = false;
		}
		if (other.gameObject.name == "wall1") {
			gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z-0.25f);
			moveSpeed = 0.0f ;
		} else if (other.gameObject.name == "wall2") {
			gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			gameObject.transform.position = new Vector3 (transform.position.x+0.25f, transform.position.y, transform.position.z);
			moveSpeed = 0.0f ;
		} else if (other.gameObject.name == "wall3") {
			gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z+0.25f);

			moveSpeed = 0.0f ;
		}  else if (other.gameObject.name == "wall4") {
			gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			gameObject.transform.position = new Vector3 (transform.position.x-0.25f, transform.position.y, transform.position.z);
			moveSpeed = 0.0f ;
			//			moveSpeed = -moveSpeed;
		}  else if (other.gameObject.name == "mouse(Clone)"&&isCarrying==false) {
			Destroy (other.gameObject);
			gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			moveSpeed = 0.0f;
			producer.GetComponent<mouse> ().mouseCounter--;
			isCarrying = true;
			gotStolen = false;
		}

		if (isCarrying == false) {
			if (other.gameObject.name == "nest3") {
				if (GameObject.Find("owl2").GetComponent<owl2> ().isOut == true && GameObject.Find("owl2").GetComponent<owl2> ().score > 0) {
					GameObject.Find("owl2").GetComponent<owl2> ().score--;
					isCarrying = true;
					gotStolen = false;
				}
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			} else if (other.gameObject.name == "nest") {
				if (GameObject.Find("owl").GetComponent<owl> ().isOut == true && GameObject.Find("owl").GetComponent<owl> ().score > 0) {
					GameObject.Find("owl").GetComponent<owl> ().score--;
					isCarrying = true;
					gotStolen = false;
				}
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			} else if (other.gameObject.name == "nest4") {
				if (GameObject.Find("owl4").GetComponent<owl4> ().isOut == true && GameObject.Find("owl4").GetComponent<owl4> ().score > 0) {
					GameObject.Find("owl4").GetComponent<owl4> ().score--;
					isCarrying = true;
					gotStolen = false;
				}
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			}
		} else {
			if (other.gameObject.name == "nest3") {
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			} else if (other.gameObject.name == "nest") {
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			} else if (other.gameObject.name == "nest4") {
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			}
		}

		if (isCarrying == false&&gotStolen==false) {
			if (other.gameObject.name == "owl2") {
				if (other.gameObject.GetComponent<owl2> ().isCarrying == true) {
					isFighting = true;
					moveSpeed = 0.0f;
					fightPro.GetComponent<cloud> ().pos = gameObject.transform.position;
					fightPro.GetComponent<cloud> ().isProducing = true;
					other.gameObject.GetComponent<owl2> ().isCarrying = false;
					other.gameObject.GetComponent<owl2> ().gotStolen = true;
					isCarrying = true;
				}
			} else if (other.gameObject.name == "owl") {
				if (other.gameObject.GetComponent<owl> ().isCarrying == true) {
					isFighting = true;
					moveSpeed = 0.0f;
					fightPro.GetComponent<cloud> ().pos = gameObject.transform.position;
					fightPro.GetComponent<cloud> ().isProducing = true;
					other.gameObject.GetComponent<owl> ().isCarrying = false;
					other.gameObject.GetComponent<owl> ().gotStolen = true;
					isCarrying = true;
				}

			} else if (other.gameObject.name == "owl4") {
				if (other.gameObject.GetComponent<owl4> ().isCarrying == true) {
					isFighting = true;
					moveSpeed = 0.0f;
					fightPro.GetComponent<cloud> ().pos = gameObject.transform.position;
					fightPro.GetComponent<cloud> ().isProducing = true;
					other.gameObject.GetComponent<owl4> ().isCarrying = false;
					other.gameObject.GetComponent<owl4> ().gotStolen = true;
					isCarrying = true;
				}

			}
		}
	}
}

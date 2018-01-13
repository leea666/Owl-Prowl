using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class owl : MonoBehaviour {
	private SerialPort serialPort = null ;
	private String portName = "/dev/cu.usbmodem1421";
	private int baudRate = 115200;
	private int readTimeOut = 100;

	private string serialInput;

	bool programActive = true;
	Thread thread;
	public float potA ;
	public float potB ;
	public float potC;
	public float potD;

	public float pressure1 ;
	public float pressure2 ;
	public float pressure3 ;
	public float pressure4 ;

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
		Debug.developerConsoleVisible = true;
		rotateSpeed = 3.0f;
		moveSpeed = 0.0f;
		setSpeed = 0.2f;
		oriPos = gameObject.transform.position;
		oriRot = gameObject.transform.rotation;
		oriColor = GameObject.Find ("nesttop").GetComponent<Renderer> ().material.color;
		oriNest = GameObject.Find ("nesttop").GetComponent<Renderer> ().material.color;
		isFighting = false;
		producer = GameObject.Find ("MouseProducer");
		fightPro = GameObject.Find ("CloudProducer");
		scorePad = GameObject.Find ("score-1");
		isCarrying = false;
		score = 0;
		gotStolen = false;

		try
		{
			Debug.LogError("Hi") ;
			serialPort = new SerialPort();
			serialPort.PortName = portName;
			serialPort.BaudRate = baudRate;
			serialPort.ReadTimeout = readTimeOut;
			serialPort.Open();
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
		thread = new Thread(new ThreadStart(ProcessData));
		thread.Start();
	}

	void ProcessData()
	{
		Debug.LogError("Thread: Start");
		while (programActive)
		{
			try
			{
				serialInput = serialPort.ReadLine();
			}
			catch (TimeoutException)
			{

			}
		}
		Debug.LogError("Thread: Stop");
	}

	void Update() {
		if (serialInput != null) {
			string[] strEul = serialInput.Split (',');
			if (strEul.Length > 0) {

				pressure1 = float.Parse (strEul [0]);
				Debug.LogError ("got input");
				potA = float.Parse (strEul [1]);
				pressure2 = float.Parse (strEul [2]);
				potB = float.Parse (strEul [3]);
				pressure3 = float.Parse (strEul [4]);
				potC = float.Parse (strEul [5]);
				pressure4 = float.Parse (strEul [6]);
				potD = float.Parse (strEul [7]);
			}
		}
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
//				potA++;
//			} else if (Input.GetKey (KeyCode.D)) {
//				potA--;
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

			Vector3 destination = new Vector3 (0, potA, 0);
			gameObject.transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, 
				destination, 
				Time.deltaTime*rotateSpeed);
			if (pressure1 >= 900) {
				if (!isOut) {
					isOut = true;
				}
				moveSpeed = setSpeed;
			}

			scorePad.GetComponent<UnityEngine.UI.Text> ().text = "score  " + score * 10;

			gameObject.transform.Translate (new Vector3 (-moveSpeed, 0.0f, 0.0f));

			if (isCarrying == true) {
				GameObject.Find ("owlbody").GetComponent<Renderer> ().material.color = new Color (0.0f, 149.0f/255.0f *  Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f, Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f);
			} else {
				GameObject.Find ("owlbody").GetComponent<Renderer> ().material.color = new Color (0.0f, 149.0f/255.0f , 1.0f);
			}

			if (isOut && score > 0) {
				GameObject.Find ("nesttop").GetComponent<Renderer> ().material.color = new Color (0.0f, 0.0f, Mathf.Sin (Time.time * 10.0f) / 4.0f + 0.5f);
			} else {
				GameObject.Find ("nesttop").GetComponent<Renderer> ().material.color = oriNest;
			}
		} else if (state == 0) {
			rotateSpeed = 1.0f;
			moveSpeed = 0.0f;
			setSpeed = 0.2f;
			gameObject.transform.position = oriPos;
			isFighting = false;
			isCarrying = false;
			gotStolen = false;
//			scorePad.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		} else if (state == 2) {
			score = 0;
			scorePad.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		}
	}

	public void OnDisable()
	{
		programActive = false;
		if (serialPort != null && serialPort.IsOpen)
			serialPort.Close();
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other);
		if (other.gameObject.name == "nest") {
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
		} else if (other.gameObject.name == "mouse(Clone)"&&isCarrying==false) {
			Destroy (other.gameObject);
			gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			moveSpeed = 0.0f;
			producer.GetComponent<mouse> ().mouseCounter--;
			isCarrying = true;
			gotStolen = false;
		} 

		if (isCarrying == false) {
			if (other.gameObject.name == "nest2") {
				if (GameObject.Find("owl2").GetComponent<owl2> ().isOut == true && GameObject.Find("owl2").GetComponent<owl2> ().score > 0) {
					GameObject.Find("owl2").GetComponent<owl2> ().score--;
					isCarrying = true;
					gotStolen = false;
				}
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			} else if (other.gameObject.name == "nest3") {
				if (GameObject.Find("owl3").GetComponent<owl3> ().isOut == true && GameObject.Find("owl3").GetComponent<owl3> ().score > 0) {
					GameObject.Find("owl3").GetComponent<owl3> ().score--;
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
			if (other.gameObject.name == "nest2") {
				gameObject.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
			} else if (other.gameObject.name == "nest3") {
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
			} else if (other.gameObject.name == "owl3") {
				if (other.gameObject.GetComponent<owl3> ().isCarrying == true) {
					isFighting = true;
					moveSpeed = 0.0f;
					fightPro.GetComponent<cloud> ().pos = gameObject.transform.position;
					fightPro.GetComponent<cloud> ().isProducing = true;
					other.gameObject.GetComponent<owl3> ().isCarrying = false;
					other.gameObject.GetComponent<owl3> ().gotStolen = true;
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

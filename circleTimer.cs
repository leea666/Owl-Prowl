using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class circleTimer : MonoBehaviour {
		public float state = 1 ;
		Image fillImg;
		float timeAmt = 120;
		float time;
		public Text timeText; 
		bool doOnce ;
		public bool resetPos ;
	
		// Use this for initialization
		void Start () {
			state = 2;
			fillImg = this.GetComponent<Image>();
			time = 120;
			doOnce = true ;
			resetPos = false ;

		}
	
		// Update is called once per frame
		void Update () {
			if (state == 1) {
			fillImg.GetComponent<Image> ().enabled = true;
				if (time > 0) {
					time -= Time.deltaTime;
					fillImg.fillAmount = time / timeAmt; 
					timeText.text = "Time : " + time.ToString ("F");  
				}
				doOnce = true;
				resetPos = false;
			} else if (state == 0 && doOnce == true) {
				time = 7.5f;
				doOnce = false;
			} else if (state == 0) {
				fillImg.GetComponent<Image> ().enabled = false;

				if (time > 0) {
					time -= Time.deltaTime;
				}
			} else if (state == 2) {
				fillImg.GetComponent<Image> ().enabled = false;
			}

			if (time <= 0 && state == 1) {
				state = 0;
			} else if (time <= 0 && state == 0) {
				state = 2;
				doOnce = true;
			}

			GameObject owl1 = GameObject.Find ("owl");
			owl getpressure1 = owl1.GetComponent<owl> ();
			float pressure1 = getpressure1.pressure1;
			float pressure2 = getpressure1.pressure2;
			float pressure3 = getpressure1.pressure3;
			float pressure4 = getpressure1.pressure4;
			if (state == 2 && (pressure1 > 900 && pressure2 > 900 && pressure3 > 900 && pressure4 > 900)) {
				state = 1;
				time = 120;
				resetPos = true;
			} else if (Input.GetKeyDown (KeyCode.B)) {
				state = 1;
				time = 120;
			}
		}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class Timer : MonoBehaviour {
//	public float timeLeft = 120f ;
	public float timeLeft = 10f ;
	GameObject timer;
	GameObject timer2;
	// Use this for initialization
	void Start () {
		timer = GameObject.Find ("timer");
		timer2 = GameObject.Find ("timer (1)"); 
		timeLeft = 120f;
	}

	// Update is called once per frame
	void Update () {
//		timer.GetComponent<UnityEngine.UI.Text> ().text = timeLeft.ToString();
		GameObject circleTime = GameObject.Find ("Image");
		circleTimer getState = circleTime.GetComponent<circleTimer> ();
		float state = getState.state;

		if (state == 1) {
			timer.GetComponent<Text> ().enabled = true;
			timer2.GetComponent<Text> ().enabled = true;
			timeLeft -= Time.deltaTime;
			UpdateLevelTimer (timeLeft);
		} else if (state == 0) {
			timer.GetComponent<Text> ().enabled = false;
			timer2.GetComponent<Text> ().enabled = false;
		} else if (state == 2) {
			timer.GetComponent<Text> ().enabled = false;
			timer2.GetComponent<Text> ().enabled = false;
			timeLeft = 120f;
		}
	}

	public void UpdateLevelTimer(float totalSeconds)
	{
		int minutes = Mathf.FloorToInt(totalSeconds / 60f);
		int seconds = Mathf.RoundToInt(totalSeconds % 60f);

		string formatedSeconds = seconds.ToString();

		if (seconds == 60)
		{
			seconds = 0;
			minutes += 1;
		}

		timer.GetComponent<UnityEngine.UI.Text>().text = minutes.ToString("0") + ":" + seconds.ToString("00");
		timer2.GetComponent<UnityEngine.UI.Text>().text = minutes.ToString("0") + ":" + seconds.ToString("00");
	}
//	

}

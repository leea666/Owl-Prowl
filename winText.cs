using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class winText : MonoBehaviour {
	GameObject winShow ;
	GameObject winShow2 ;
	GameObject winShow3 ;
	GameObject winShow4 ;
	GameObject titleShow ;
	// Use this for initialization
	void Start () {
		winShow = GameObject.Find ("win");
		winShow2 = GameObject.Find ("win (1)");
		winShow3 = GameObject.Find ("win (2)");
		winShow4 = GameObject.Find ("win (3)");
		titleShow = GameObject.Find ("title");
	}
	
	// Update is called once per frame
	void Update () {
		GameObject circleTime = GameObject.Find ("Image");
		circleTimer getState = circleTime.GetComponent<circleTimer> ();
		float state = getState.state;

		if (state == 1) {
			winShow.GetComponent<Text> ().enabled = false;
			winShow2.GetComponent<Text> ().enabled = false;
			winShow3.GetComponent<Text> ().enabled = false;
			winShow4.GetComponent<Text> ().enabled = false;
			titleShow.GetComponent<Text> ().enabled = false;
		} else if (state == 0) {
			titleShow.GetComponent<Text> ().enabled = false;
			GameObject score = GameObject.Find ("owl");
			owl showScore = score.GetComponent<owl> ();
			float barnardoScore = showScore.score;
			GameObject score2 = GameObject.Find ("owl2");
			owl2 showScore2 = score2.GetComponent<owl2> ();
			float hootyScore = showScore2.score;
			GameObject score3 = GameObject.Find ("owl3");
			owl3 showScore3 = score3.GetComponent<owl3> ();
			float tawnyScore = showScore3.score;
			GameObject score4 = GameObject.Find ("owl4");
			owl4 showScore4 = score4.GetComponent<owl4> ();
			float owliverScore = showScore4.score;
			if (barnardoScore > hootyScore && barnardoScore > tawnyScore && barnardoScore > owliverScore) {
				winShow.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow2.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow3.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow4.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
			} else if (hootyScore > barnardoScore && hootyScore > tawnyScore && hootyScore > owliverScore) {
				winShow.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow2.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow3.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow4.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
			} else if (tawnyScore > barnardoScore && tawnyScore > hootyScore && tawnyScore > owliverScore) {
				winShow.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow2.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow3.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow4.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
			} else if (owliverScore > barnardoScore && owliverScore > tawnyScore && owliverScore > hootyScore) {
				winShow.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow2.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow3.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
				winShow4.GetComponent<UnityEngine.UI.Text> ().text = "WINNER";
			} else if (barnardoScore == hootyScore || barnardoScore == tawnyScore || barnardoScore == owliverScore
			         	|| hootyScore == tawnyScore || hootyScore == owliverScore || tawnyScore == owliverScore) {
				winShow.GetComponent<UnityEngine.UI.Text> ().text = "DRAW";
				winShow2.GetComponent<UnityEngine.UI.Text> ().text = "DRAW";
				winShow3.GetComponent<UnityEngine.UI.Text> ().text = "DRAW";
				winShow4.GetComponent<UnityEngine.UI.Text> ().text = "DRAW";
			} else {
				
			}
			winShow.GetComponent<Text> ().enabled = true;
			winShow2.GetComponent<Text> ().enabled = true;
			winShow3.GetComponent<Text> ().enabled = true;
			winShow4.GetComponent<Text> ().enabled = true;
		} else if (state == 2) {
			winShow.GetComponent<Text> ().enabled = false;
			winShow2.GetComponent<Text> ().enabled = false;
			winShow3.GetComponent<Text> ().enabled = false;
			winShow4.GetComponent<Text> ().enabled = false;
			titleShow.GetComponent<Text> ().enabled = true;
		}
	}
}

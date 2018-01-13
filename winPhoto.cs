using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class winPhoto : MonoBehaviour {
	GameObject winShow ;
	GameObject rulesShow ;
	public Sprite barnardo ;
	public Sprite tawny ;
	public Sprite owliver ;
	public Sprite hooty ;

	// Use this for initialization
	void Start () {
		winShow = GameObject.Find ("winnersPhoto");
		rulesShow = GameObject.Find ("rules");
//		barnardo =  Resources.Load <Sprite>("barnardo");
//		tawny =  Resources.Load <Sprite>("tawny");
//		owliver =  Resources.Load <Sprite>("owliver");
//		hooty =  Resources.Load <Sprite>("hooty");
	}
	
	// Update is called once per frame
	void Update () {
		winShow.transform.Rotate (new Vector3 (0.0f, 0.0f, Mathf.Sin (Time.time * 100.0f) / 4.0f + 6.5f));

		GameObject circleTime = GameObject.Find ("Image");
		circleTimer getState = circleTime.GetComponent<circleTimer> ();
		float state = getState.state;

		if (state == 1) {
			rulesShow.GetComponent<Image> ().enabled = false;
			winShow.GetComponent<Image> ().enabled = false;

		} else if (state == 0) {
			rulesShow.GetComponent<Image> ().enabled = false;
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
				winShow.GetComponent<Image> ().sprite = barnardo;
				winShow.GetComponent<Image> ().enabled = true;
			} else if (hootyScore > barnardoScore && hootyScore > tawnyScore && hootyScore > owliverScore) {
				winShow.GetComponent<Image> ().sprite = hooty;
				winShow.GetComponent<Image> ().enabled = true;
			} else if (tawnyScore > barnardoScore && tawnyScore > hootyScore && tawnyScore > owliverScore) {
				winShow.GetComponent<Image> ().sprite = tawny;
				winShow.GetComponent<Image> ().enabled = true;
			} else if (owliverScore > barnardoScore && owliverScore > tawnyScore && owliverScore > hootyScore) {
				winShow.GetComponent<Image> ().sprite = owliver;
				winShow.GetComponent<Image> ().enabled = true;
			} else if (barnardoScore == hootyScore || barnardoScore == tawnyScore || barnardoScore == owliverScore
			           || hootyScore == tawnyScore || hootyScore == owliverScore || tawnyScore == owliverScore) {
				winShow.GetComponent<Image> ().enabled = false;
			}

		} else if (state == 2) {
			winShow.GetComponent<Image> ().enabled = false;
			rulesShow.GetComponent<Image> ().enabled = true;

		}
		
	}
}

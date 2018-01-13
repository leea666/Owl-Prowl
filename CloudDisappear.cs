using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDisappear: MonoBehaviour {
	int timeCount;
	int disappearCount;
	public int maxTime;
	public bool isFighting;

	// Use this for initialization
	void Start () {
		timeCount = 0;
		disappearCount = 0;
		maxTime = 200;
		isFighting = true;
	}
	
	// Update is called once per frame
	void Update () {
		timeCount++;
		if (timeCount >= maxTime || isFighting == false) {
			gameObject.GetComponent<ParticleSystem> ().loop = false;
			isFighting = false;
			disappearCount++;
			if (disappearCount >= 300) {
				Destroy (gameObject);
			}
		}
	}
}

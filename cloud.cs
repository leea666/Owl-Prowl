using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour {
	public GameObject cloudPrefab;
	public bool isProducing;
	public Vector3 pos;
	private Quaternion randomRot;

	// Use this for initialization
	void Start () {
		isProducing = false;
		randomRot = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isProducing == true) {
			GameObject tempCloud = Instantiate (cloudPrefab, pos, randomRot);
			isProducing = false;
		}
	}
}

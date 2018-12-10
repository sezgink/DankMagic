using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaretScript : MonoBehaviour {
	float totalTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;
		transform.localPosition = new Vector3(0, 2.5f +Mathf.Sin (totalTime*2.5f)/1.8f,0);
	}
}

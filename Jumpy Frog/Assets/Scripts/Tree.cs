using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.gameObject.transform.position;

		if (Frog.movementSpeed != 0) {
			pos.x -= Frog.movementSpeed / 2;
		} else {
			pos.x -= 0;
		}

		if (pos.x <= -40) {
			pos.x = 120;
		}

		this.gameObject.transform.position = pos;
	}
}

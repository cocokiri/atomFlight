using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Proton : MonoBehaviour {

    // Use this for initialization
    private Vector3 gravityCenter = new Vector3(0, 0, 0);
    public float g = 2;

    Rigidbody body;

    void Start() {
        gravityCenter = transform.parent.transform.position;
        body = GetComponent<Rigidbody>();
    }


	
	// Update is called once per frame
	void Update () {
        body.AddForce((transform.localPosition) * -g);
		
	}
}

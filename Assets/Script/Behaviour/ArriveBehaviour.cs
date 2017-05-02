using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBehaviour : SteeringBehaviour {
    public GameObject target;
    public float slowingDistance = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Vector3 Calculate()
    {
        return boid.ArriveForce(this.target.transform.position, this.slowingDistance);
    }
}

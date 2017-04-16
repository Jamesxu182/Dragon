using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour {
    public Vector3 target;

	// Use this for initialization
	void Start () {
        if(target == null)
        {
            this.target = Vector3.zero;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Vector3 Calculate() {
        return SeekForce();
    }


    public Vector3 SeekForce()
    {
        Vector3 desired = this.target - transform.position;
        desired.Normalize();
        desired *= boid.maxSpeed;
        return desired - boid.velocity;
    }
}

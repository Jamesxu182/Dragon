using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidController : SteeringBehaviour {
    private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public override Vector3 Calculate()
    {
        if(transform.position.y <= 0.0f)
        {
            if(boid.velocity.y <= 0.0f)
            {
                boid.velocity.y = 0.0f;
            }

            return Vector3.zero;
        }

        else
        {
            return gravity;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueBehaviour : SteeringBehaviour
{
    public Boid target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Vector3 Calculate()
    {
        float distance = Vector3.Distance(transform.position, boid.transform.position);
        float t = distance / boid.maxSpeed;

        Vector3 future_position = target.transform.position + t * target.velocity;

        return boid.SeekForce(future_position);
    }
}

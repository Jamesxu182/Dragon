using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidBehaviour : SteeringBehaviour {

    private Vector3 front_feeler;
    private Vector3 left_feeler;
    private Vector3 right_feeler;

    public float front_feeler_length = 5.0f;
    public float side_feeler_length = 5.0f;


    public Vector3 height_offset = new Vector3(0.0f, 2.5f, 0.0f);

    public float feeler_update_pre_second = 10.0f;
    public float feeler_radius = 2.0f;

    public LayerMask mask = -1;

    private bool feeler_collided = false;
    private RaycastHit front_feeler_info;
    private RaycastHit right_feeler_info;
    private RaycastHit left_feeler_info;

    private Boolean front_collided = false;
    private Boolean left_collided = false;
    private Boolean right_collided = false;

    public void OnDrawGizmos()
    {
        if(this.front_collided)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + height_offset + transform.forward, front_feeler + height_offset);
        }

        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + height_offset + transform.forward, front_feeler + height_offset);
        }

        if(this.right_collided)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + height_offset + transform.forward, right_feeler + height_offset);
        }

        else {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + height_offset + transform.forward, right_feeler + height_offset);
        }

        if(this.left_collided)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + height_offset + transform.forward, left_feeler + height_offset);
        }

        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + height_offset + transform.forward, left_feeler + height_offset);
        }
    }

    // Use this for initialization
    void Start () {
        front_feeler = boid.transform.position + boid.velocity.normalized * front_feeler_length;
        left_feeler = boid.transform.position + Quaternion.AngleAxis(-30.0f, transform.up) * boid.velocity.normalized * side_feeler_length;
        right_feeler = boid.transform.position + Quaternion.AngleAxis(30.0f, transform.up) * boid.velocity.normalized * side_feeler_length;

        StartCoroutine(UpdateFrontFeeler());
        StartCoroutine(UpdateSideFeelers());
    }
	
	// Update is called once per frame
	void Update () {
        front_feeler = boid.transform.position + boid.velocity.normalized * front_feeler_length;
        left_feeler = boid.transform.position + Quaternion.AngleAxis(-30.0f, transform.up) * boid.velocity.normalized * side_feeler_length;
        right_feeler = boid.transform.position + Quaternion.AngleAxis(30.0f, transform.up) * boid.velocity.normalized * side_feeler_length;
    }

    System.Collections.IEnumerator UpdateFrontFeeler()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 0.5f));
        while(true)
        {
            RaycastHit info;
            this.front_collided = Physics.SphereCast(boid.transform.position + height_offset + transform.forward, feeler_radius, front_feeler.normalized, out info, front_feeler_length - feeler_radius, mask.value);

            if (this.front_collided)
            {
                this.feeler_collided = true;
                this.front_feeler_info = info;
            }

            else
            {
                this.feeler_collided = this.front_collided && this.right_collided && this.left_collided;
            }

            yield return new WaitForSeconds(1.0f / feeler_update_pre_second);
        }
    }

    System.Collections.IEnumerator UpdateSideFeelers()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 0.5f));

        while (true)
        {
            RaycastHit left_info;
            RaycastHit right_info;

            this.left_collided = Physics.SphereCast(boid.transform.position + height_offset + transform.forward, feeler_radius, left_feeler.normalized, out left_info, side_feeler_length - feeler_radius, mask.value);
            this.right_collided = Physics.SphereCast(boid.transform.position + height_offset + transform.forward, feeler_radius, right_feeler.normalized, out right_info, side_feeler_length - feeler_radius, mask.value);

            if (this.left_collided)
            {
                this.feeler_collided = true;
                this.left_feeler_info = left_info;
            }

            if(this.right_collided)
            {
                this.feeler_collided = true;
                this.right_feeler_info = right_info;
            }

            if(this.left_collided)
            {
                this.feeler_collided = this.front_collided;
            }

            yield return new WaitForSeconds(1.0f / feeler_update_pre_second);
        }
    }

    private Vector3 AvoidForce(RaycastHit feeler_info, float feeler_length)
    {
        Vector3 force = Vector3.zero;

        Vector3 fromTarget = boid.transform.position - feeler_info.point;
        float distance = fromTarget.magnitude;

        force = feeler_info.normal * (feeler_length * 1.5f / distance);

        return Vector3.Scale(force, new Vector3(1.0f, 0.0f, 1.0f));
    }

    public override Vector3 Calculate()
    {
        Vector3 force = Vector3.zero;

        if(this.feeler_collided)
        {
            if(this.front_collided)
            {
                force += AvoidForce(front_feeler_info, front_feeler_length);
            }

            if(this.right_collided)
            {
                force += AvoidForce(right_feeler_info, side_feeler_length);
            }

            if(this.left_collided)
            {
                force += AvoidForce(left_feeler_info, side_feeler_length);
            }
        }

        return force;
    }
}

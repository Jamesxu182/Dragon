using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehaviour : SteeringBehaviour {

    public Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);

    private Vector3 gravity_force = Vector3.zero;

    private Vector3 offset = Vector3.up;

    public float gravity_update_pre_second = 10.0f;

    public LayerMask mask = -1;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(UpdateGravity());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override Vector3 Calculate()
    {
        return this.gravity_force;
    }

    System.Collections.IEnumerator UpdateGravity()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 0.5f));
        while (true)
        {
            RaycastHit info;
            bool collided = Physics.Raycast(transform.position + offset, -Vector3.up, out info);

            if (collided)
            {
                if (info.distance <= offset.magnitude)
                {
                    // On the groud
                    this.gravity_force = Vector3.zero;
                }

                else
                {
                    this.gravity_force = this.gravity;
                }

            }

            else
            {

            }

            yield return new WaitForSeconds(1.0f / gravity_update_pre_second);
        }
    }

}

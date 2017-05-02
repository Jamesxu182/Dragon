using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : SteeringBehaviour {

    private Vector3 jump_force = Vector3.zero;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            this.jump_force = Vector3.up * Time.deltaTime * 10000.0f;
        }

        else
        {
            this.jump_force = Vector3.zero;
        }

        return jump_force;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidControllerBehaviour : SteeringBehaviour {
    public float amplitude = 90.0f;
    public float force_magnitude = 10.0f;

    private float angle = 0.0f;
    private Vector3 control_force = Vector3.zero;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , 10.0f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + control_force.normalized * 10.0f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float movement_h = Input.GetAxis("Movement Horizontal");
        float ps_movement_h = Input.GetAxis("PS Movement Horizontal");
        float gravity_movement_h = Vector3.Dot(Input.gyro.gravity, Vector3.right) / 1.0f;

        movement_h += Mathf.Abs(ps_movement_h) > 0.05f ? ps_movement_h : 0.0f;

        angle = movement_h == 0.0f ? 0.0f : Mathf.Clamp((movement_h / 1.0f), -1.0f, 1.0f) * amplitude;
        angle += gravity_movement_h;

        Debug.Log(angle);
    }

    public override Vector3 Calculate()
    {
        control_force = Quaternion.AngleAxis(angle, transform.up) * transform.forward * force_magnitude;
        return control_force;
    }
}

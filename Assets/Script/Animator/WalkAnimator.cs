using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimator : MonoBehaviour {

    public GameObject left_hand;
    public GameObject right_hand;

    public GameObject left_leg;
    public GameObject right_leg;

    public float leg_amplitude = 0.5f;
    public float hand_amplitude = 30.0f;
    public float frequency = 10.0f;
    public float default_frequency = 10.0f;

    private Vector3 left_leg_original_position;
    private Vector3 left_leg_front_position;
    private Vector3 left_leg_back_position;

    private Vector3 right_leg_original_position;
    private Vector3 right_leg_front_position;
    private Vector3 right_leg_back_position;

    private Vector3 left_hand_original_direction;
    private Vector3 left_hand_front_direction;
    private Vector3 left_hand_back_direction;

    private Vector3 right_hand_original_direction;
    private Vector3 right_hand_front_direction;
    private Vector3 right_hand_back_direction;

    private float theta = 0.0f;

    private Boid boid;

    // Use this for initialization
    void Start () {
        this.frequency = this.default_frequency;

        if(this.left_hand)
        {
            this.left_hand_original_direction = transform.InverseTransformDirection(this.left_hand.transform.up);
            this.left_hand_front_direction = Quaternion.AngleAxis(-hand_amplitude, this.left_hand.transform.right) * this.left_hand_original_direction;
            this.left_hand_back_direction = Quaternion.AngleAxis(hand_amplitude, this.left_hand.transform.right) * this.left_hand_original_direction;
        }

        if (this.right_hand)
        {
            this.right_hand_original_direction = transform.InverseTransformDirection(this.right_hand.transform.up);
            this.right_hand_front_direction = Quaternion.AngleAxis(-hand_amplitude, this.right_hand.transform.right) * this.right_hand_original_direction;
            this.right_hand_back_direction = Quaternion.AngleAxis(hand_amplitude, this.right_hand.transform.right) * this.right_hand_original_direction;
        }

        if(this.left_leg)
        {
            this.left_leg_original_position = transform.InverseTransformPoint(this.left_leg.transform.position);
            this.left_leg_front_position = this.left_leg_original_position + new Vector3(0.0f, 0.0f, this.leg_amplitude);
            this.left_leg_back_position = this.left_leg_original_position - new Vector3(0.0f, 0.0f, this.leg_amplitude);
        }

        if (this.right_leg)
        {
            this.right_leg_original_position = transform.InverseTransformPoint(this.right_leg.transform.position);
            this.right_leg_front_position = this.right_leg_original_position + new Vector3(0.0f, 0.0f, this.leg_amplitude);
            this.right_leg_back_position = this.right_leg_original_position - new Vector3(0.0f, 0.0f, this.leg_amplitude);
        }

        boid = GetComponent<Boid>();
    }
	
	// Update is called once per frame
	void Update () {
        Walk();
	}

    private void Walk()
    {
        this.theta += Time.deltaTime * this.frequency;
        float sin = Mathf.Sin(this.theta);

        if (boid.velocity.magnitude > 0.1f)
        {

            this.frequency = Mathf.Clamp(boid.velocity.magnitude, 0.5f, 1.0f) * this.default_frequency;

            if (sin > 0)
            {
                this.left_leg.transform.position = transform.TransformPoint(Vector3.Lerp(transform.InverseTransformPoint(this.left_leg.transform.position), left_leg_front_position, sin));
                this.right_leg.transform.position = transform.TransformPoint(Vector3.Lerp(transform.InverseTransformPoint(this.right_leg.transform.position), right_leg_back_position, sin));

                this.left_hand.transform.up = transform.TransformDirection(Vector3.Slerp(transform.InverseTransformDirection(this.left_hand.transform.up), this.left_hand_front_direction, sin));
                this.right_hand.transform.up = transform.TransformDirection(Vector3.Slerp(transform.InverseTransformDirection(this.right_hand.transform.up), this.right_hand_back_direction, sin));
            }

            else
            {
                this.left_leg.transform.position = transform.TransformPoint(Vector3.Lerp(transform.InverseTransformPoint(this.left_leg.transform.position), left_leg_back_position, -sin));
                this.right_leg.transform.position = transform.TransformPoint(Vector3.Lerp(transform.InverseTransformPoint(this.right_leg.transform.position), right_leg_front_position, -sin));

                this.left_hand.transform.up = transform.TransformDirection(Vector3.Slerp(transform.InverseTransformDirection(this.left_hand.transform.up), this.left_hand_back_direction, -sin));
                this.right_hand.transform.up = transform.TransformDirection(Vector3.Slerp(transform.InverseTransformDirection(this.right_hand.transform.up), this.right_hand_front_direction, -sin));
            }
        }

        else
        {
            this.left_leg.transform.position = transform.TransformPoint(Vector3.Lerp(transform.InverseTransformPoint(this.left_leg.transform.position), left_leg_original_position, sin));
            this.right_leg.transform.position = transform.TransformPoint(Vector3.Lerp(transform.InverseTransformPoint(this.right_leg.transform.position), right_leg_original_position, sin));

            this.left_hand.transform.up = transform.TransformDirection(Vector3.Slerp(transform.InverseTransformDirection(this.left_hand.transform.up), this.left_hand_original_direction, sin));
            this.right_hand.transform.up = transform.TransformDirection(Vector3.Slerp(transform.InverseTransformDirection(this.right_hand.transform.up), this.right_hand_original_direction, sin));
        }
    }
}

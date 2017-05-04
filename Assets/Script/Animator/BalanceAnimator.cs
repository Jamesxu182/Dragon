using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceAnimator : MonoBehaviour {
    public float amplitude = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.up != Vector3.up)
        {
            //UpdateTargetVector();
            //float smoothRate = Mathf.Clamp(5.0f * Time.deltaTime, 0.05f, 0.45f);
            //transform.up = Vector3.Slerp(transform.up, target_vector, smoothRate);

            //float angle = Mathf.Abs(Vector3.Angle(Vector3.up, transform.up));

            transform.up = Vector3.up;
        }
	}

    void UpdateTargetVector()
    {
        /*float angle = Mathf.Abs(Vector3.Angle(Vector3.up, transform.up));

        if(angle < this.amplitude)
        {
            this.target_vector = transform.up;
        }

        else
        {
            this.target_vector = Quaternion.AngleAxis(amplitude, Vector3.Cross(Vector3.up, transform.up)) * Vector3.up;
        }*/
    }
}

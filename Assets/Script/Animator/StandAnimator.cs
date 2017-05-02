using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandAnimator : MonoBehaviour {
    private Vector3 offset = Vector3.up;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit info;

        bool collided = Physics.Raycast(transform.position + offset, -Vector3.up, out info);

        if (collided)
        {
            if (info.distance <= offset.magnitude)
            {
                // On the groud
            }

            else
            {
                transform.Translate(new Vector3(0.0f, offset.magnitude - info.distance, 0.0f));
            }

        }

        else
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinAnimator : MonoBehaviour {

    public GameObject left_fin;
    public GameObject right_fin;

    private Vector3 left_fin_offset;
    private Vector3 right_fin_offset;

	// Use this for initialization
	void Start () {
        left_fin_offset = left_fin.transform.position - transform.position;
        left_fin_offset = Quaternion.Inverse(transform.rotation) * left_fin_offset;

        right_fin_offset = right_fin.transform.position - transform.position;
        right_fin_offset = Quaternion.Inverse(transform.rotation) * right_fin_offset;
    }

    // Update is called once per frame
    void Update () {

	}
}

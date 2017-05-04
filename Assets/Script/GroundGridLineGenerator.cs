using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGridLineGenerator : MonoBehaviour {
    public float ground_start_x = -50.0f;
    public float ground_end_x = 50.0f;
    public float ground_start_y = -50.0f;
    public float ground_end_y = 50.0f;

    public float gap = 5.0f;

	public GameObject target;

	private Vector3 centre = Vector3.zero;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for(float i = this.ground_start_x; i <= this.ground_end_x; i += gap)
        {
            Gizmos.DrawLine(centre + new Vector3(i, 0.0f, ground_start_y), centre + new Vector3(i, 0.0f, ground_end_y));
        }

        for (float j = this.ground_start_y; j <= this.ground_end_y; j += gap)
        {
            Gizmos.DrawLine(centre + new Vector3(ground_start_x, 0.0f, j), centre + new Vector3(ground_end_x, 0.0f, j));
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCentrePosition ();
	}

	private void UpdateCentrePosition()
	{
		if (target) {
			centre = target.transform.position;

			centre.x = Mathf.Round(centre.x / gap) * gap;
			centre.y = Mathf.Round(centre.y / gap) * gap;
			centre.z = Mathf.Round(centre.z / gap) * gap;
		}
	}
}

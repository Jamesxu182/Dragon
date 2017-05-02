using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodViewCameraController : MonoBehaviour {
    public Vector3 init_position = new Vector3(50.0f, 30.0f, -50.0f);
    public Vector3 init_rotation = new Vector3(30.0f, -45.0f, 0.0f);

    public Path waypoints;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;

        transform.transform.position = init_position;
        transform.Rotate(init_rotation);
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = true;

        //float cursor_v = Input.GetAxis("");
        //float cursor_y = Input.GetAxis("");

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);

                Vector3 target_point = hit.point;

                if(waypoints)
                {
                    GameObject new_point = new GameObject();
                    new_point.transform.position = target_point;

                    waypoints.AddNewPoint(new_point);
                }
            }
        }

        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            Debug.Log(touch.position);
        }
    }
}

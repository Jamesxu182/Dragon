using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float movement_speed = 20.0f;
    public float movment_lerp_rate = 10.0f;
    public float view_speed = 150.0f;

    public bool isEnableLerp = false;

    private Vector3 movement_target;
    private Vector3 relative_movement_target;

    private Vector3 view_target;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.movement_target, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.view_target + transform.forward * 1.0f, 0.01f);
    }

    // Use this for initialization
    void Start () {
        this.relative_movement_target = Vector3.zero;
        this.movement_target = transform.position;
        this.view_target = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = false;


        float movement_h = Input.GetAxis("Movement Horizontal");
        float movement_v = Input.GetAxis("Movement Vertical");

        float ps_movement_h = Input.GetAxis("PS Movement Horizontal");
        float ps_movement_v = Input.GetAxis("PS Movement Vertical");

        movement_h += Mathf.Abs(ps_movement_h) > 0.05f  ? ps_movement_h : 0.0f;
        movement_v += Mathf.Abs(ps_movement_v) > 0.05f ? ps_movement_v : 0.0f;

        Vector3 add_relative_movement_target = new Vector3(movement_h * movement_speed * Time.deltaTime, 0, movement_v * movement_speed * Time.deltaTime);

        if(isEnableLerp)
        {
            if (!add_relative_movement_target.Equals(Vector3.zero))
            {
                this.relative_movement_target += add_relative_movement_target;
                this.movement_target = transform.TransformPoint(relative_movement_target);
            }

            else
            {
                this.relative_movement_target += add_relative_movement_target;
                this.movement_target = transform.TransformPoint(relative_movement_target);
            }

            float smoothRate = Mathf.Clamp(movment_lerp_rate * Time.deltaTime, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(transform.position, this.movement_target, smoothRate);
        }

        else
        {
            this.movement_target = transform.TransformPoint(relative_movement_target);
            transform.Translate(new Vector3(movement_h * movement_speed * Time.deltaTime, 0, movement_v * movement_speed * Time.deltaTime));
        }


        float view_h = Input.GetAxis("View Horizontal");
        float view_v = Input.GetAxis("View Vertical");

        float ps_view_h = Input.GetAxis("PS View Horizontal");
        float ps_view_v = Input.GetAxis("PS View Vertical");

        float mouse_h = Input.GetAxis("Mouse Horizontal");
        float mouse_v = Input.GetAxis("Mouse Vertical");

        view_h += Mathf.Abs(ps_view_h) > 0.05f ? ps_view_h : 0.0f;
        view_v += Mathf.Abs(ps_view_v) > 0.05f ? ps_view_v : 0.0f;

        view_h += mouse_h;
        view_v += mouse_v;

        this.view_target = transform.TransformPoint(new Vector3(0.0f, 0.0f, 0.0f));

        transform.RotateAround(transform.position, transform.right, view_v * view_speed * Time.deltaTime);
        transform.RotateAround(transform.position, transform.up, view_h * view_speed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
    }
}

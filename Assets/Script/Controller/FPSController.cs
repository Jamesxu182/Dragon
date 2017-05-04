using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
    public float view_speed = 10.0f;

    public float movement_speed = 20.0f;
    public float rotation_speed = 100.0f;

    public float explosion_radius = 5.0f;

    private float theta = -Mathf.PI / 2; //θ
    private float fi = -(1.0f / 4.0f) * Mathf.PI; //φ

    private Vector3 camera_relative_position;
    private Vector3 camera_position;
    private Vector3 camera_direction;

    private Vector3 offset = new Vector3(0.0f, 10.0f, 0.0f);

    private GameObject camera_object;

    [HideInInspector]
    public bool is_moving = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + this.offset, explosion_radius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.camera_position, 0.1f);
    }

    // Use this for initialization
    void Start () {
        Cursor.visible = false;

        this.camera_object = Camera.main.gameObject;

        offset = new Vector3(0.0f, explosion_radius, 0.0f);

        UpdateCameraPosition();
        UpdateCameraDirection();
    }
	
	// Update is called once per frame
	void Update () {

        Walk();
        View();
    }

    private void UpdateCameraPosition()
    {
        if (this.fi != (Mathf.PI / 2.0f))
        {
            float x = Mathf.Cos(this.theta);
            float y = Mathf.Tan(this.fi);
            float z = Mathf.Sin(this.theta);

            camera_relative_position = new Vector3(x, y, z).normalized * explosion_radius;
        }

        // this.camera_position = transform.TransformPoint(camera_relative_position) + this.offset;
        this.camera_position = transform.position + camera_relative_position + this.offset;
        camera_object.transform.position = camera_position;
    }

    private void UpdateCameraDirection()
    {
        camera_direction = (transform.position - camera_object.transform.position + new Vector3(0.0f, 3.0f, 0.0f)).normalized;

        camera_object.transform.forward = camera_direction;
    }

    private void GetViewInput()
    {
        float view_h = Input.GetAxis("View Horizontal");
        float view_v = Input.GetAxis("View Vertical");

        float ps_view_h = Input.GetAxis("PS View Horizontal");
        float ps_view_v = Input.GetAxis("PS View Vertical");

        float mouse_h = Input.GetAxis("Mouse Horizontal");
        float mouse_v = Input.GetAxis("Mouse Vertical");

		Debug.Log ("Mouse H: " + mouse_h);
		Debug.Log ("Mouse V: " + mouse_v);

		if (Mathf.Abs(mouse_h) > 5.0f || Mathf.Abs(mouse_v) > 20.0f) {
			mouse_h = 0.0f;
			mouse_v = 0.0f;
		}

        view_h += Mathf.Abs(ps_view_h) > 0.05f ? ps_view_h : 0.0f;
        view_v += Mathf.Abs(ps_view_v) > 0.05f ? ps_view_v : 0.0f;

        view_h += mouse_h;
        view_v += mouse_v;

        this.theta += Mathf.Deg2Rad * view_h * view_speed;
        this.fi += Mathf.Deg2Rad * view_v * view_speed;

        this.theta = this.theta % (2 * Mathf.PI);
        this.fi = Mathf.Clamp(this.fi, -Mathf.PI / 3.0f, (Mathf.PI / 2));
    }

    public void View()
    {
        GetViewInput();
        UpdateCameraPosition();
        UpdateCameraDirection();
    }

    private void Walk()
    {
        float movement_h = Input.GetAxis("Movement Horizontal");
        float movement_v = Input.GetAxis("Movement Vertical");

        float ps_movement_h = Input.GetAxis("PS Movement Horizontal");
        float ps_movement_v = Input.GetAxis("PS Movement Vertical");

        movement_h += Mathf.Abs(ps_movement_h) > 0.05f ? ps_movement_h : 0.0f;
        movement_v += Mathf.Abs(ps_movement_v) > 0.05f ? ps_movement_v : 0.0f;

        if(movement_v != 0)
        {
            this.is_moving = true;
        }

        else
        {
            this.is_moving = false;
        }

        transform.Translate(new Vector3(0.0f, 0.0f, movement_v * movement_speed * Time.deltaTime));
        transform.Rotate(new Vector3(0.0f, movement_h * rotation_speed * Time.deltaTime, 0.0f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float movement_speed = 20.0f;
    public float view_speed = 150.0f;

    private void OnDrawGizmos()
    {
        ;
    }

    // Use this for initialization
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;


        float movement_h = Input.GetAxis("Movement Horizontal");
        float movement_v = Input.GetAxis("Movement Vertical");

        float ps_movement_h = Input.GetAxis("PS Movement Horizontal");
        float ps_movement_v = Input.GetAxis("PS Movement Vertical");

        movement_h += Mathf.Abs(ps_movement_h) > 0.05f ? ps_movement_h : 0.0f;
        movement_v += Mathf.Abs(ps_movement_v) > 0.05f ? ps_movement_v : 0.0f;
        transform.Translate(new Vector3(movement_h * movement_speed * Time.deltaTime, 0, movement_v * movement_speed * Time.deltaTime));

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

        transform.RotateAround(transform.position, transform.right, view_v * view_speed * Time.deltaTime);
        transform.RotateAround(transform.position, transform.up, view_h * view_speed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathBehaviour : SteeringBehaviour {

    public Path path;

    private Boolean is_end = false;

    public void OnDrawGizmos()
    {
        if(path.GetCount() > 0)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(boid.transform.position, path.NextWaypoint());
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Vector3 Calculate()
    {
        is_end = false;
        if(path.GetCount() > 0)
        {
            Vector3 nextWaypoint = path.NextWaypoint();

            if (Vector3.Distance(transform.position, nextWaypoint) < 1.0f)
            {
                path.AdvanceToNext();
            }

            if (!path.looped && path.isLast())
            {
                if (Vector3.Distance(transform.position, nextWaypoint) < 0.5f)
                {
                    path.Clear();

                    foreach (Transform child in path.GetChilds())
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    is_end = true;
                }

                return boid.ArriveForce(nextWaypoint, 10);
            }

            else
            {
                return boid.SeekForce(nextWaypoint);
            }
        }

        else
        {
            return Vector3.zero;
        }
    }

    public Boolean IsEnd()
    {
        return is_end;
    }
}

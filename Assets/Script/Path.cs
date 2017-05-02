using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public List<Vector3> waypoints = new List<Vector3>();

    public int next = 0;
    public bool looped = true;

    public void OnDrawGizmos()
    {
        int count = looped ? (transform.childCount + 1) : transform.childCount;
        Gizmos.color = Color.cyan;

        if(GetCount() >= 1)
        {
            Gizmos.DrawSphere(NextWaypoint(), 0.5f);
        }

        for (int i = this.next + 1; i < count; i++)
        {
            Transform prev = transform.GetChild(i - 1);
            Transform next = transform.GetChild(i % transform.childCount);
            Gizmos.DrawLine(prev.transform.position, next.transform.position);
            Gizmos.DrawSphere(prev.position, 0.5f);
            Gizmos.DrawSphere(next.position, 0.5f);
        }
    }

    // Use this for initialization
    void Start()
    {
        waypoints.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            waypoints.Add(transform.GetChild(i).position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 NextWaypoint()
    {
        return waypoints[next];
    }

    public void AdvanceToNext()
    {
        if (looped)
        {
            next = (next + 1) % waypoints.Count;
        }
        else
        {
            if (next != waypoints.Count - 1)
            {
                next++;
            }
        }
    }

    public bool isLast()
    {
        return next == waypoints.Count - 1;
    }

    public void AddNewPoint(GameObject new_point)
    {
        new_point.transform.parent = transform;

        waypoints.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            waypoints.Add(transform.GetChild(i).position);
        }
    }

    public int GetCount()
    {
        return transform.childCount;
    }

    public void Clear()
    {
        next = 0;
        waypoints.Clear();
    }

    public List<Transform> GetChilds()
    {
        List<Transform> childs = new List<Transform>();

        int count = looped ? (transform.childCount + 1) : transform.childCount;
        for (int i = 0; i < count; i++)
        {
            childs.Add(transform.GetChild(i));
        }

        return childs;
    }
}

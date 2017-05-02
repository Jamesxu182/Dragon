using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadBehaviour : SteeringBehaviour {
    public Boid leader;
    public float behind_offset = 5.0f;

    public Transform parent_transform;

    public float separation_radius = 2.0f;

    private Vector3 tv;
    private Vector3 behind;
    private Vector3 ahead;

    private List<Boid> followers = new List<Boid>();

    private int neighbour_count = 0;
    private float max_separation = 10.0f;
    private float leader_sight_radius = 5.0f;



	// Use this for initialization
	void Start () {
        tv = leader.velocity * -1;
        tv = tv.normalized * behind_offset;
        behind = leader.transform.position + tv;

        for(int i = 0; i < parent_transform.childCount; i++)
        {
            followers.Add(parent_transform.GetChild(i).GetComponent<Boid>());
        }

        tv = leader.velocity;
        tv = tv.normalized * behind_offset;
        ahead = leader.transform.position + tv;
	}
	
	// Update is called once per frame
	void Update () {
        tv = leader.velocity * -1;
        tv = tv.normalized * behind_offset;
        behind = leader.transform.position + tv;

        tv = leader.velocity;
        tv = tv.normalized * behind_offset;
        ahead = leader.transform.position + tv;
    }

    public override Vector3 Calculate()
    {
        Vector3 force = Vector3.zero;

        if(IsOnLeaderSight())
        {
            force += boid.EvadeForce(leader.transform.position);
        }

        force += boid.ArriveForce(behind, 5.0f);
        force += Separation();

        return force;
    }

    public Vector3 Separation()
    {
        Vector3 force = Vector3.zero;

        foreach(Boid follower in followers)
        {
            if(follower != boid && Vector3.Distance(follower.transform.position, boid.transform.position) < separation_radius)
            {
                force = follower.transform.position - boid.transform.position;

                neighbour_count++;
            }
        }

        if(neighbour_count != 0)
        {
            force.Scale(Vector3.one * (- 1.0f * neighbour_count));
        }

        Vector3.Normalize(force);
        force.Scale(Vector3.one * max_separation);

        return force;
    }

    public bool IsOnLeaderSight()
    {
        return Vector3.Distance(leader.transform.position, boid.transform.position) < leader_sight_radius || Vector3.Distance(ahead, boid.transform.position) < leader_sight_radius;
    }
}

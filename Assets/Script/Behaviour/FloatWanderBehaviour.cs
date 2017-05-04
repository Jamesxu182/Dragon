using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWanderBehaviour : SteeringBehaviour {
    public float radius = 10.0f;
    public float distance = 15.0f;
    public float jitter = 5.0f;
    public float force_update_pre_second = 10.0f;

    private Vector3 target = Vector3.zero;
    private Vector3 wander_force = Vector3.zero;

    private void OnDrawGizmos()
    {
        if(boid != null && boid.velocity != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + boid.velocity.normalized * distance, radius);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + boid.velocity.normalized * distance, 0.5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + boid.velocity.normalized * distance, boid.transform.TransformPoint(target + Vector3.forward * distance));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, boid.transform.TransformPoint(target + Vector3.forward * distance));
        }
    }

    public void Start()
    {   
        target = Quaternion.AngleAxis(90.0f, Vector3.right) * Random.insideUnitCircle * radius;

        StartCoroutine(UpdateWanderForce());
    }

    System.Collections.IEnumerator UpdateWanderForce()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 0.5f));
        while (true)
        {
            float jitterTimeSlice = jitter * Time.deltaTime;

            Vector3 toAdd = Quaternion.AngleAxis(90.0f, Vector3.right) * Random.insideUnitCircle * jitterTimeSlice;
            target += toAdd;
            target.Normalize();
            target *= radius;

            Vector3 localTarget = target + Vector3.forward * distance;

            Vector3 worldTarget = boid.transform.TransformPoint(localTarget);

            wander_force = Vector3.Scale((worldTarget - boid.transform.position), new Vector3(0.1f, 0.0f, 0.1f));

            yield return new WaitForSeconds(1.0f / force_update_pre_second);
        }
    }

    public override Vector3 Calculate()
    {
        return wander_force;
    }
}

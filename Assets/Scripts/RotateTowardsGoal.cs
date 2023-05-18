using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsGoal : MonoBehaviour
{
    public GameObject goal;

    public float speed;

    public float rotationModifier;

    private void FixedUpdate()
    {
        if (goal != null)
        {
            Vector3 vectorToTarget = goal.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }

    }
}

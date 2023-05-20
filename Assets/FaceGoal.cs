using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGoal : MonoBehaviour
{
    Transform tr;
    Transform parentTr;
    public float rotation;
    float oldRot;
    public Transform goal;
    float xdif;
    float ydif;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        parentTr = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xdif = goal.position.x - tr.position.x;
        ydif = goal.position.y - tr.position.y;
        if (xdif > 0.01)
        {
            rotation = Mathf.Atan(ydif / xdif);
            rotation = rotation * 180 / Mathf.PI;
        }
        else if (ydif == 0 && xdif > 0.01)
        {
            rotation = 0;
        }
        else if (ydif == 0 && xdif < -0.01)
        {
            rotation = 180;
        }
        else if (xdif == 0 && ydif > 0.01)
        {
            rotation = 90;
        }
        else if (xdif == 0 && ydif < -0.01)
        {
            rotation = 270;
        }
        else if (xdif < -0.01 && xdif > 0.01 && ydif < -0.01 && ydif > 0.01)
        {
            rotation = oldRot;
        }
        else if (xdif < -0.01)
        {
            rotation = Mathf.Atan(ydif / xdif);
            rotation = (rotation * 180 / Mathf.PI) + 180;
        }
        //Debug.Log(oldy + " , " + parentTr.position.y + " , " + GetVelocity(oldy, parentTr.position.y));
        //Debug.Log(oldx + " , " + parentTr.position.x + " , " + GetVelocity(oldx, parentTr.position.x));
        tr.rotation = Quaternion.Euler(tr.rotation.x, tr.rotation.y, rotation);
        oldRot = rotation;
    }
}

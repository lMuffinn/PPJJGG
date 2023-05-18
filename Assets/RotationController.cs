using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{

    Transform tr;
    Transform parentTr;
    float oldx;
    float oldy;
    public float rotation;
    public float xvel;
    public float yvel;
    float oldRot;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        parentTr = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xvel = GetVelocity(oldx, parentTr.position.x);
        yvel = GetVelocity(oldy, parentTr.position.y);
        if (xvel > 0.01)
        {
            rotation = Mathf.Atan(yvel / xvel);
            rotation = rotation * 180 / Mathf.PI;
        }
        else if (yvel == 0 && xvel > 0.01)
        {
            rotation = 0;
        }
        else if (yvel == 0 && xvel < -0.01)
        {
            rotation = 180;
        }
        else if (xvel == 0 && yvel > 0.01)
        {
            rotation = 90;
        }
        else if (xvel == 0 && yvel < -0.01)
        {
            rotation = 270;
        }
        else if (xvel < -0.01 && xvel > 0.01 && yvel < -0.01 && yvel > 0.01)
        {
            rotation = oldRot;
        }
        else if (xvel < -0.01)
        {
            rotation = Mathf.Atan(yvel / xvel);
            rotation = (rotation * 180 / Mathf.PI) + 180;
        }
        //Debug.Log(oldy + " , " + parentTr.position.y + " , " + GetVelocity(oldy, parentTr.position.y));
        //Debug.Log(oldx + " , " + parentTr.position.x + " , " + GetVelocity(oldx, parentTr.position.x));
        tr.rotation = Quaternion.Euler(tr.rotation.x, tr.rotation.y, rotation);
        oldx = parentTr.position.x;
        oldy = parentTr.position.y;
        oldRot = rotation;
    }

    float GetVelocity(float oldpos, float pos)
    {
        float velocity = pos-oldpos;
        return velocity;
    }

}

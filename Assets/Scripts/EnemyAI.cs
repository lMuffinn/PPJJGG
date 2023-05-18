using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;










//DO NOT USE! this is the origional ai, it is bad. maybe for your team but otherwise no.













public class EnemyAI : MonoBehaviour
{
    AIDestinationSetter AID;
    //bool hasPuck;
    public Transform behind;
    public Transform front;
    public Collider2D checkIfBehind;
    //public LayerMask puck;
    public LayerMask behindLayer;
    //variables for way is clear
    //public Transform puck;
    //public Transform goal;
   // public LayerMask obstacle;

    // Start is called before the first frame update
    void Start()
    {
        AID = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkIfBehind.IsTouchingLayers(behindLayer))
        {
            AID.target = front;
            Debug.Log("got it");
        }
        else
        {
            AID.target = behind;
        }
    }

    //bool WayIsClear()
    //{
        //Check if the way is clear between the puck and the goal.

        //return Physics2D.Linecast(puck.position,);
    //}
}

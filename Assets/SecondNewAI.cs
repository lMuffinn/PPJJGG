using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SecondNewAI : MonoBehaviour
{

    public Transform goal;
    public Transform puck;
    PuckController pc;
    AIDestinationSetter AID;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PuckController>();
        AID = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.hasPuck)
        {
            AID.target = goal;
        }
        else
        {
            AID.target = puck;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAnger : MonoBehaviour
{

    public float anger = 3;
    AIDestinationSetter aID;
    public Transform player;
    public Collider2D hitRange;

    // Start is called before the first frame update
    void Start()
    {
        aID = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anger == 0)
        {
            aID.target = player;
        }
    }

    public void Smack()
    {
        Debug.Log("Smacked");
        anger--;
    }

}

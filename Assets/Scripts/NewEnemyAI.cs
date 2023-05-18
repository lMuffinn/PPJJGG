using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewEnemyAI : MonoBehaviour
{
    public Transform target;//
    public float pathUpdateSeconds = 0.5f;//
    public float speed = 200f;//
    public float nextWaypointDistance = 3f;//
    private Path path;//
    private int currentWaypoint = 0;//
    bool ReachedEndOfPath = false;//
    Seeker seeker;//
    Rigidbody2D rb;//
    //----------------------------------------------------
    [Header("Custom")]
    public Transform goal;
    public Transform puck;
    PuckController pc;

    public void Start()
    {
        seeker = GetComponent<Seeker>();//
        rb = GetComponent<Rigidbody2D>();//

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);//
        //---------------------------------------------------
        pc = GetComponent<PuckController>();
    }

    void Update()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            return;
        }
        else
        {
            ReachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        //-----------------------------------------------------------
        if (pc.hasPuck)
        {
            target = goal;
        }
        else
        {
            target = puck;
        }
    }

    private void UpdatePath()//
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)//
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}

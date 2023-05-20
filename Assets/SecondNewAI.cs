using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SecondNewAI : MonoBehaviour
{

    public Transform goal;
    public Transform puck;
    public Transform playerT;
    PuckController pc;
    AIDestinationSetter AID;
    public LayerMask player;
    //Transform moveAround;
    //Transform moveAround2;
    public LayerMask obstacle;
    SecondNewAI[] enemyTeammatesList;
    List<SecondNewAI> enemyTeammates;
    public bool closest = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PuckController>();
        AID = GetComponent<AIDestinationSetter>();
        enemyTeammatesList = FindObjectsOfType<SecondNewAI>();
        foreach(SecondNewAI enemy in enemyTeammatesList)
        {
            enemyTeammates.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //GetClosestToPuck(enemyTeammates, puck).close
        {
            Debug.Log("it worked");
        }
        RaycastHit2D hitPlayer = Physics2D.Linecast(GetComponent<Transform>().position,puck.position,player);
        bool hit = Physics2D.Linecast(GetComponent<Transform>().position, puck.position, player);
        RaycastHit2D hitPlayerGoal = Physics2D.Linecast(GetComponent<Transform>().position, goal.position, player);
        bool hitGoal = Physics2D.Linecast(GetComponent<Transform>().position, goal.position, player);
        //Debug.Log(hit);
        Debug.DrawLine(GetComponent<Transform>().position, puck.position,Color.yellow);
        Debug.DrawLine(GetComponent<Transform>().position, goal.position, Color.red);
        //Debug.Log(hitPlayer);
        if (!hitGoal && pc.hasPuck)
        {
            AID.target = goal;
        }
        else if (hitGoal && pc.hasPuck)
        {
            List<Transform> goTo = new List<Transform>();
            goTo.Add(hitPlayerGoal.transform.Find("Puck Controll").Find("Enemy Avoidance").Find("enemy go to").GetComponent<Transform>());
            goTo.Add(hitPlayerGoal.transform.Find("Puck Controll").Find("Enemy Avoidance").Find("enemy go to2").GetComponent<Transform>());
            bool m1 = goTo[0].gameObject.GetComponent<Collider2D>().IsTouchingLayers(obstacle);
            bool m2 = goTo[1].gameObject.GetComponent<Collider2D>().IsTouchingLayers(obstacle);
            if (m1 && !m2)
            {
                AID.target = goTo[1];
            }
            else if (!m1 && m2)
            {
                AID.target = goTo[0];
            }
            else if (m1 && m2)
            {
                AID.target = playerT;
            }
            else
            {
                AID.target = GetClosest(goTo, this.transform);
            }
        }
        else if (!hit && !pc.hasPuck)
        {
            AID.target = puck;
        }
        else if (hit && !pc.hasPuck)
        {
            //Go around player if they are in the way
            //Debug.Log("player between");
            List<Transform> goTo = new List<Transform>();
            goTo.Add(hitPlayer.transform.Find("Puck Controll").Find("Puck Controller").Find("enemy go to").GetComponent<Transform>());
            goTo.Add(hitPlayer.transform.Find("Puck Controll").Find("Puck Controller").Find("enemy go to2").GetComponent<Transform>());
            bool m1 = goTo[0].gameObject.GetComponent<Collider2D>().IsTouchingLayers(obstacle);
            bool m2 = goTo[1].gameObject.GetComponent<Collider2D>().IsTouchingLayers(obstacle);
            if(m1 && !m2)
            {
                AID.target = goTo[1];
            }
            else if (!m1 && m2)
            {
                AID.target = goTo[0];
            }
            else if (m1 && m2)
            {
                AID.target = playerT;
            }
            else
            {
                AID.target = GetClosest(goTo,this.transform);
            }
        }
    }

    Transform GetClosest(List<Transform> targets, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    Transform GetClosestToPuck(List<SecondNewAI> targets, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (SecondNewAI potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.gameObject.GetComponent<Transform>().position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.gameObject.GetComponent<Transform>();
            }
        }
        return bestTarget;
    }

}

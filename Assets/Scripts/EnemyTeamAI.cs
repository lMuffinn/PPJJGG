using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTeamAI : MonoBehaviour
{
    public List<Transform> enemyTeammates;
    SecondNewAI oldClostest;
    SecondNewAI currentClosest;
    public Transform puck;
    public List<Transform> defenseLocations;
    public List<Transform> offenseLocations;
    public PuckController player;

    // Start is called before the first frame update
    void Start()
    {
        oldClostest = enemyTeammates[0].gameObject.GetComponent<SecondNewAI>();
    }

    // Update is called once per frame
    void Update()
    {
        List<AIDestinationSetter> notClosest = new List<AIDestinationSetter>();
        //Debug.Log(enemyTeammates);
        currentClosest = GetClosest(enemyTeammates, puck).gameObject.GetComponent<SecondNewAI>();
        currentClosest.closest = true;
        if (currentClosest != oldClostest)
        {
            oldClostest.closest = false;
        }
        oldClostest = currentClosest;
        foreach (Transform mate in enemyTeammates)
        {
            if (!mate.gameObject.GetComponent<SecondNewAI>().closest && mate.gameObject.GetComponent<EnemyAnger>().anger != 0 && !mate.GetComponent<Caught>().caught)
            {
                notClosest.Add(mate.gameObject.GetComponent<AIDestinationSetter>());
            }
        }
        if (player.hasPuck || GameObject.FindObjectOfType<FriendController>().offense)
        {
            for (int i = 0; i < notClosest.Count; i++)
            {
            notClosest[i].target = defenseLocations[i];
            }
        }
        else
        {
            for (int i = 0; i < notClosest.Count; i++)
            {
                notClosest[i].target = offenseLocations[i];
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smack : MonoBehaviour
{

    public Collider2D smackRange;
    public LayerMask Enemylayer;
    PuckController pc;
    public List<Transform> enemyTeammates;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PuckController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (smackRange.IsTouchingLayers(Enemylayer) && Input.GetKeyDown(KeyCode.Space) && !pc.hasPuck)
        {
            GetClosest(enemyTeammates, this.transform).gameObject.GetComponent<EnemyAnger>().Smack();
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

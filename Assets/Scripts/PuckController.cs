using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{

    public GameObject puck;
    public Transform puckPosition;
    public float puckSpeed = 10f;
    public bool hasPuck = false;
    public Collider2D puckSteal;
    public Collider2D puckPickUp;
    public LayerMask puckLayer;
    public bool player;
    RotationController rc;
    Rigidbody2D rb;
    public float strength = 10;
    float shootTimer = 0;
    public float shootCooldown = 1;
    PuckController[] players;
    public LayerMask shoot;
    public LayerMask turnSolid;
    bool needToBeSolid = false;
    public Transform rayOrigion;
    public float puckDistance;
    public float puckOffset;
    public Transform backupPosition;
    float stealTimer = 0;
    float stealCooldown;


    // Start is called before the first frame update
    void Start()
    {
        rc = GetComponentInChildren<RotationController>();
        rb = GetComponent<Rigidbody2D>();
        players = FindObjectsOfType<PuckController>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Quaternion.AngleAxis(rc.rotation, transform.forward) * transform.right;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigion.position,direction,puckDistance,turnSolid);
        Debug.DrawRay(rayOrigion.position, direction * puckDistance,Color.blue);
        if (hit)
        {
            puckPosition.position = new Vector2(hit.point.x,hit.point.y);
        }
        else
        {
            puckPosition.position = backupPosition.position;
        }
        //Check if anyone has the puck
        bool puckTaken = false;
        foreach (PuckController guy in players)
        {
            if (guy.hasPuck)
            {
                puckTaken = true;
            }
        }
        //if puck is touching a wall turn solid
        /*if (puck.GetComponentInChildren<Collider2D>().IsTouchingLayers(turnSolid))
        {
            needToBeSolid = true;
            puck.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            needToBeSolid = false;
        }*/
        //Debug.Log(puckTaken);
        if (puckPickUp.IsTouchingLayers(puckLayer) && shootTimer < 0 && !puckTaken)
        {
            //Grab the puck if no one already has it.
            hasPuck = true;
        }
        //if you have the puck move it in front of you
        if (hasPuck)
        {
            puck.GetComponent<Collider2D>().isTrigger = true;
            puck.GetComponent<Transform>().position = Vector3.Lerp(puck.GetComponent<Transform>().position,puckPosition.position,puckSpeed);
        }
        if (puckSteal.IsTouchingLayers(puckLayer) && !hasPuck)
        {
            //Steal the puck.
            foreach (PuckController guy in players)
            {
                if (guy.hasPuck)
                {
                    guy.hasPuck = false;
                }
            }
            hasPuck = true;
        }
        //take away ownership of the puck if it is out of range
        if (!puckPickUp.IsTouchingLayers(puckLayer))
        {
            hasPuck = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && hasPuck && player)
        {
            //Shoot the puck.
            hasPuck = false;
            shootTimer = shootCooldown;
            puck.GetComponent<Collider2D>().isTrigger = false;
            Vector3 dir = Quaternion.AngleAxis(rc.rotation, Vector3.forward) * Vector3.right;
            puck.GetComponent<Rigidbody2D>().AddForce(dir * strength);
            Debug.Log("shot");
        }
        //shoot the puck but for the enemy
        if (!player && puck.GetComponent<Collider2D>().IsTouchingLayers(shoot) && hasPuck)
        {
            hasPuck = false;
            shootTimer = shootCooldown;
            puck.GetComponent<Collider2D>().isTrigger = false;
            Vector3 dir = Quaternion.AngleAxis(rc.rotation, Vector3.forward) * Vector3.right;
            puck.GetComponent<Rigidbody2D>().AddForce(dir * strength);
            //Debug.Log("shot");
        }
        shootTimer -= Time.deltaTime;
    }
}

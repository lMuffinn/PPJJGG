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
    float timer;
    public float shootCooldown = 1;
    PuckController[] players;
    public LayerMask shoot;

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
        bool puckTaken = false;
        foreach (PuckController guy in players)
        {
            if (guy.hasPuck)
            {
                puckTaken = true;
            }
        }
        //Debug.Log(puckTaken);
        if (puckPickUp.IsTouchingLayers(puckLayer) && timer < 0 && !puckTaken)
        {
            //Grab the puck if no one already has it.
            hasPuck = true;
        }
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
        if (!puckPickUp.IsTouchingLayers(puckLayer))
        {
            hasPuck = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && hasPuck && player)
        {
            //Shoot the puck.
            hasPuck = false;
            timer = shootCooldown;
            puck.GetComponent<Collider2D>().isTrigger = false;
            Vector3 dir = Quaternion.AngleAxis(rc.rotation, Vector3.forward) * Vector3.right;
            puck.GetComponent<Rigidbody2D>().AddForce(dir * strength);
            Debug.Log("shot");
        }
        if (!player && puck.GetComponent<Collider2D>().IsTouchingLayers(shoot) && hasPuck)
        {
            hasPuck = false;
            timer = shootCooldown;
            puck.GetComponent<Collider2D>().isTrigger = false;
            Vector3 dir = Quaternion.AngleAxis(rc.rotation, Vector3.forward) * Vector3.right;
            puck.GetComponent<Rigidbody2D>().AddForce(dir * strength);
            Debug.Log("shot");
        }
        timer -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Caught : MonoBehaviour
{

    public bool caught = false;
    public bool player = false;
    public Transform shameCorner;
    public float timeoutTime = 5;
    public float timer = 0;
    public GameObject hey;
    public float heyTimerTime = 1;
    public float heyTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (caught && !player)
        {
            GetComponent<SecondNewAI>().enabled = false;
            GetComponent<PuckController>().enabled = false;
            GetComponent<AIDestinationSetter>().target = shameCorner;
        }
        if (caught && player)
        {
            if (timer > 0)
            {
                GetComponent<Transform>().position = shameCorner.position;
            }
            else
            {
                caught = false;
            }
        }
        if (heyTimer > 0)
        {
            hey.SetActive(true);
        }
        else
        {
            hey.SetActive(false);
        }
        timer-=Time.deltaTime;
        heyTimer-= Time.deltaTime;
    }
}

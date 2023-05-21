using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    float xPosDef = 0;
    float yPosDef = 0;
    float xPosOff = 0;
    float yPosOff = 0;
    float timer = 0;
    public float changeTime = 5;
    public PuckController player;
    public PuckController own;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            xPosOff = Random.Range(-55, 0);
            yPosOff = Random.Range(-30, 30);
            xPosDef = Random.Range(0, 55);
            yPosDef = Random.Range(-30, 30);
            timer = changeTime;
        }
        if (player.hasPuck || GameObject.FindObjectOfType<FriendController>().offense)
        {
            GetComponent<Transform>().position = new Vector2(xPosOff,yPosOff);
        }
        else
        {
            GetComponent<Transform>().position = new Vector2(xPosDef, yPosDef);
        }
        timer -= Time.deltaTime;
    }
}

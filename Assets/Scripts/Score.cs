using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public int team1;
    public int team2;
    public GameObject team1Text;
    public GameObject team2Text;
    public Collider2D team1Goal;
    public Collider2D team2Goal;
    bool goalScored = false;
    public LayerMask puckLayer;
    public Transform puckStartPosition;
    float timer;
    public float timeBeforeNextRound = 1;
    public GameObject puck;
    public float goalCoolDown = 2;
    float cooldownTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if ((team1Goal.IsTouchingLayers(puckLayer)) && !goalScored && cooldownTimer<0)
        {
            team1++;
            goalScored = true;
            timer = timeBeforeNextRound;
            cooldownTimer = goalCoolDown;
        }
        else if ((team2Goal.IsTouchingLayers(puckLayer)) && !goalScored && cooldownTimer < 0)
        {
            team2++;
            goalScored = true;
            timer = timeBeforeNextRound;
            cooldownTimer = goalCoolDown;
        }
        else if (timer < 0 && goalScored)
        {
            puck.GetComponent<Transform>().position = puckStartPosition.position;
            puck.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            goalScored = false;
        }
        team1Text.GetComponent<TextMeshProUGUI>().text = team1.ToString();
        team2Text.GetComponent<TextMeshProUGUI>().text = team2.ToString();
        timer -= Time.deltaTime;
        cooldownTimer -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;
using UnityEngine.SceneManagement;

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
    public GameObject fadeIn;
    public List<Transform> enemys;
    public List<Transform> enemyPositions;
    public List<Transform> friends;
    public List<Transform> friendPositions;
    public float gameTimer = 300;
    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        if ((team1Goal.IsTouchingLayers(puckLayer)) && !goalScored && cooldownTimer<0)
        {
            team1++;
            goalScored = true;
            timer = timeBeforeNextRound;
            cooldownTimer = goalCoolDown;
            fadeIn.GetComponent<Animator>().SetBool("Scored", true);
        }
        else if ((team2Goal.IsTouchingLayers(puckLayer)) && !goalScored && cooldownTimer < 0)
        {
            team2++;
            goalScored = true;
            timer = timeBeforeNextRound;
            cooldownTimer = goalCoolDown;
            fadeIn.GetComponent<Animator>().SetBool("Scored", true);
        }
        else if (timer < 0 && goalScored)
        {
            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i] = enemyPositions[i];
                friends[i] = friendPositions[i];
                //Debug.Log("moved");
            }
            puck.GetComponent<Transform>().position = puckStartPosition.position;
            puck.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            goalScored = false;
            fadeIn.GetComponent<Animator>().SetBool("Scored", false);
        }
        team1Text.GetComponent<TextMeshProUGUI>().text = team1.ToString();
        team2Text.GetComponent<TextMeshProUGUI>().text = team2.ToString();
        timer -= Time.deltaTime;
        cooldownTimer -= Time.deltaTime;
        //Debug.Log(team1 > team2);
        if(gameTimer < 0 && team1 > team2)
        {
            SceneManager.LoadScene(4);
        }
        if (gameTimer < 0 && team1 > team2)
        {
            SceneManager.LoadScene(5);
        }
        if (gameTimer < 0 && team1 == team2)
        {
            SceneManager.LoadScene(7);
        }
        gameTimer -= Time.deltaTime;
        timerText.text = gameTimer.ToString("F0");
    }
}

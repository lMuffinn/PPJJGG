using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{

    Transform tr;
    float oldx;
    float oldy;
    float xVel;
    float yVel;
    Animator animator;
    SpriteRenderer sr;
    int smackTimer = 0;
    int bonkTimer = 0;
    GameObject referee;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        referee = GameObject.Find("Referee");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("yVel",GetVelocity(oldy, tr.position.y));
        animator.SetFloat("xVel",Mathf.Abs(GetVelocity(oldx, tr.position.x)));
        xVel = GetVelocity(oldx, tr.position.x);
        yVel = GetVelocity(oldy, tr.position.y);
        if (xVel > 0.01)
        {
            if (yVel < 0.01)
            {
                sr.flipX = true;
            }
            else if (yVel > -0.01)
            {
                sr.flipX = false;
            }
        }
        if (xVel < 0.01)
        {
            if (yVel < 0.01)
            {
                sr.flipX = false;
            }
            else if(yVel > -0.01)
            {
                sr.flipX = true;
            }
        }
        oldx = tr.position.x;
        oldy = tr.position.y;
        if(smackTimer < 0)
        {
            animator.SetBool("smack", false);
        }
        if(bonkTimer < 0)
        {
            animator.SetBool("bonk", false);
        }
        bonkTimer -= 1;
        smackTimer -= 1;
    }

    public void Smack()
    {
        animator.SetBool("smack", true);
        smackTimer = 2;
        if (GetComponentInParent<Collider2D>().IsTouching(referee.GetComponent<Referee>().currentCollider))
        {
            Debug.Log("uh oh");
            GetComponentInParent<Caught>().caught = true;
            GetComponentInParent<Caught>().heyTimer = GetComponentInParent<Caught>().heyTimerTime;
            GetComponentInParent<Caught>().timer = GetComponentInParent<Caught>().timeoutTime;
            referee.GetComponent<Referee>().countEnemies--;
        }
    }

    public void Bonk()
    {
        animator.SetBool("bonk", true);
        smackTimer = 2;
    }

    float GetVelocity(float oldpos, float pos)
    {
        float velocity = pos - oldpos;
        return velocity;
    }

}

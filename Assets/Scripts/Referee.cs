using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Referee : MonoBehaviour
{

    public List<Collider2D> sight;
    public List<Sprite> indecator;
    public float switchTime = 4;
    float timer = 0;
    public int current;
    public Collider2D currentCollider;
    public LayerMask player;
    public Image refHead;
    public int countEnemies = 4;

    // Start is called before the first frame update
    void Start()
    {
        current = Random.Range(0,4);
        currentCollider = sight[current];
    }

    // Update is called once per frame
    void Update()
    {
        if(countEnemies <= 0)
        {
            SceneManager.LoadScene(4);
        }
        if (timer < 0)
        {
            current = Random.Range(0, 4);
            currentCollider = sight[current];
            timer = switchTime;
        }
        timer -= Time.deltaTime;
        if (currentCollider.IsTouchingLayers(player))
        {
            refHead.sprite = indecator[0];
        }
        else
        {
            refHead.sprite = indecator[1];
        }
    }
}

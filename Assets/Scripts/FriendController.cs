using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{

    public List<PuckController> friends;
    public bool offense;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool currently = false;
        foreach (PuckController friend in friends)
        {
            if (friend.hasPuck)
            {
                currently = true;
            }
        }
        offense = currently;
    }
}

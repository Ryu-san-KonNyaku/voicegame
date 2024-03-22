using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public GameObject warpExit;
    public GameObject Player;
    public Vector3 Wpplus;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (Player.GetComponent<SimplePlayerController>().mgcharge)
            {
                Invoke("Warp", 0.2f);
            }
        }
    }
    void Warp()
    {
        Player.transform.position = warpExit.transform.position + Wpplus;
    }

}

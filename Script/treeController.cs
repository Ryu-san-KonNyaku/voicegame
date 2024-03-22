using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeController : MonoBehaviour
{
    public SimplePlayerController status;

    public GameObject pati;
    public int touchdamage = 5;
    public bool touch = false;
    // Start is called before the first frame update
    void OnParticleCollision(GameObject obj)
    {

        if (obj.transform.tag == "Redbullet")
        {
            pati.SetActive(true);
            touch = true;
            Invoke("flame", 5);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player" && touch)
        {
            status.playerHP -= touchdamage;
        }
    }
    void flame()
    {
        Destroy(this.gameObject);
    }
}

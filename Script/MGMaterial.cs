using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGMaterial : MonoBehaviour
{
    public GameClear gc;
    public int materialHP = 100;

    // Update is called once per frame
    void Update()
    {
        if (materialHP <= 0)
        {
            Break();
        }
    }
    void Break()
    {
        gc.count++;
        Destroy(this.gameObject);
    }
    void OnParticleCollision(GameObject obj)
    {
        if (obj.transform.tag == "air")
        {
            materialHP -= 1;
        }
        if (obj.transform.tag == "Redbullet")
        {
            materialHP -= 3;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Rock")
        {
            Break();
        }
    }
}

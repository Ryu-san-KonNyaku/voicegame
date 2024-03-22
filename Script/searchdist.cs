using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchdist : MonoBehaviour
{
    public bool pmd = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            pmd = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ice;
    void OnParticleCollision(GameObject obj)
    {
        if (obj.transform.tag == "frieze")
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            ice.SetActive(true);
        }
    }
}

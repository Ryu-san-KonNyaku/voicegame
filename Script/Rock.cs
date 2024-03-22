using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool airhit;
    private Rigidbody2D rd;
    Vector3 nposition;
    // Start is called before the first frame update
    void Start()
    {
        rd = this.GetComponent<Rigidbody2D>();
        nposition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnParticleCollision(GameObject obj)
    {
        if (obj.transform.tag == "air")
        {
            if (!airhit)
            {
                airhit = true;
                rd.constraints = RigidbodyConstraints2D.None;
                Invoke("Remove", 5f);
                rd.AddForce(new Vector2(((this.transform.position.x - obj.transform.position.x) / Mathf.Abs(this.transform.position.x - obj.transform.position.x)) * 20f, 9f), ForceMode2D.Impulse);
            }
        }

    }
    void Remove()
    {
        rd.constraints = RigidbodyConstraints2D.FreezePositionX;
        airhit = false;
        this.transform.position = nposition;
    }

}

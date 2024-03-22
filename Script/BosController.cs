using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosController : MonoBehaviour
{
    public int hitPoint;
    public float moveSpeed;
    public float accelerator;
    public float attackPower;
    public int firepoint = 5;
    public int airpoint = 20;
    public float slow;

    private float timeCount;
    private float rotation = 0;
    //public bool pmd = false;
    public GameObject target;
    public GameObject mob;
    public Animator animMob;
    private float dis = 0f;
    private bool attackb = false;
    public float mdist = 0f;
    public float defaultmoveSpeed;
    private Rigidbody2D rb;
    private bool alive = true;
    private bool damage = false;
    float rotationy;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultmoveSpeed = moveSpeed;
        animMob = GetComponent<Animator>();
    }
    void Update()
    {
        if (alive)
        {
            Vector2 posT = target.transform.position;
            Vector2 posM = mob.transform.position;
            if (Distance(posT, posM) <= mdist)
                Attack();
            if (!attackb)
                Chase(posT, posM);
            Hit();
        }
    }
    void Hit()
    {
        if (damage)
            animMob.SetTrigger("attack");
    }

    float Distance(Vector2 pT, Vector2 pM)
    {
        dis = Vector2.Distance(pT, pM);//dis=20が望ましい
        return dis;
    }
    void Attack()
    {
        if (!attackb)
        {
            if (Random.value <= 0.5)
            {
                animMob.SetTrigger("attack");
            }
            else
            {
                animMob.SetTrigger("attack02");

            }
            attackb = true;
            Invoke("Rattackb", 2f);
        }
    }
    void Rattackb()
    {
        attackb = false;
    }
    void Chase(Vector2 pT, Vector2 pM)
    {
        float pTM = pT.x - pM.x;
        if (pTM < 0)
        {
            Vector2 course = new Vector2(0, 0);
            transform.localRotation = Quaternion.Euler(course);
            rotation = -1f;
        }
        if (pTM > 0)
        {
            Vector2 course = new Vector2(0, 180);
            transform.localRotation = Quaternion.Euler(course);
            rotation = 1f;
        }

        float pTMy = pT.y - pM.y;
        if (pTMy < 0)
        {
            rotationy = -1f;
        }
        if (pTMy > 0)
        {
            rotationy = 1f;
        }

        animMob.SetBool("walk", true);
        transform.position = transform.position + new Vector3(moveSpeed * rotation * accelerator, moveSpeed * rotationy * accelerator, 0) * Time.deltaTime;
    }

}

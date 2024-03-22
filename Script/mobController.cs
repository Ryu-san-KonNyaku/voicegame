using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobController : mobStatus
{
    public int maxHP;
    private float chargeTime = 5.0f;
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
    private bool airhit = false;
    private bool alive = true;
    GameObject child;
    private bool cameracheck = false;
    void Start()
    {
        child = transform.GetChild(1).gameObject;
        rb = GetComponent<Rigidbody2D>();
        defaultmoveSpeed = moveSpeed;
        animMob = GetComponent<Animator>();
    }
    void OnWillRenderObject()
    {
#if UNITY_EDITOR
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
#endif
        {
            cameracheck = true;
        }
        //cameracheck = false;
    }
    void OnParticleCollision(GameObject obj)
    {
        if (obj.transform.tag == "air")
        {
            Debug.Log("airhit");
            if (!airhit)
            {
                airhit = true;
                Invoke("Ahit", 0.5f);
                hitPoint -= airpoint;
                rb.AddForce(new Vector2(((this.transform.position.x - obj.transform.position.x) / Mathf.Abs(this.transform.position.x - obj.transform.position.x)) * 20f, 9f), ForceMode2D.Impulse);
            }
        }
        if (obj.transform.tag == "frieze")
        {
            Debug.Log("friezehit");
            moveSpeed = slow;
            Invoke("Slow", 5f);
        }
        if (obj.transform.tag == "Redbullet")
        {
            Debug.Log("Redbullethit");
            hitPoint -= firepoint;
        }

    }
    void Ahit()
    {
        airhit = false;
    }
    void Slow()
    {
        moveSpeed = defaultmoveSpeed;
    }
    void Update()
    {
        //cameracheck = true;
        if (cameracheck)
        {
            if (alive)
            {
                Vector2 posT = target.transform.position;
                Vector2 posM = mob.transform.position;
                if (pmd)
                {
                    if (Distance(posT, posM) <= mdist)
                        Attack();
                    if (!attackb)
                        Chase(posT, posM);
                }
                else if (!pmd)
                    RunW();
                Death();
            }
        }

    }
    float Distance(Vector2 pT, Vector2 pM)
    {
        dis = Vector2.Distance(pT, pM);//dis=20が望ましい
        return dis;
    }
    void Death()
    {
        if (hitPoint <= 0)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            animMob.SetTrigger("mob_Death");
            alive = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            child.GetComponent<CircleCollider2D>().enabled = false;
            Invoke("Relive", 30f);
        }
    }
    void Relive()
    {
        hitPoint = maxHP;
        this.GetComponent<BoxCollider2D>().enabled = true;
        child.GetComponent<CircleCollider2D>().enabled = true;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        alive = true;
        animMob.SetTrigger("mob_Relive");
    }
    void RunW()
    {
        timeCount += Time.deltaTime;
        if (rotation != 0f)
        {
            animMob.SetBool("mob_Run", true);
        }
        else
        {
            animMob.SetBool("mob_Run", false);
        }
        // 自動前進
        transform.position = transform.position + new Vector3(moveSpeed * rotation, 0, 0) * Time.deltaTime;
        // 指定時間の経過（条件）
        if (timeCount > chargeTime)
        {
            float rand = Random.value;
            if (rand < 0.3)
            {
                Vector2 course = new Vector2(0, 180);
                transform.localRotation = Quaternion.Euler(course);
                rotation = 1f;
            }
            if (0.3 <= rand && rand < 0.6)
            {
                Vector2 course = new Vector2(0, 0);
                transform.localRotation = Quaternion.Euler(course);
                rotation = -1f;
            }
            if (rand >= 0.6)
            {
                rotation = 0f;
            }
            // タイムカウントを０に戻す
            timeCount = 0;
        }
    }
    void Attack()
    {
        animMob.SetTrigger("mob_Attack");
        attackb = true;
        Invoke("Rattackb", 0.5f);
    }
    void Rattackb()
    {
        attackb = false;
    }
    void Chase(Vector2 pT, Vector2 pM)
    {
        pmd = false;
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
        animMob.SetBool("mob_Run", true);
        transform.position = transform.position + new Vector3(moveSpeed * rotation * accelerator, 0, 0) * Time.deltaTime;
    }

}
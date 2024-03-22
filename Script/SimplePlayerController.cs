using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;   //Windowsの音声認識で使用
using System.Linq;
    public class SimplePlayerController : MonoBehaviour
    {
        public GameObject bgm;
        public AudioSource audioSource;
        [SerializeField]
        AudioClip[] magicSound;
        [SerializeField]
        AudioClip[] runSound;
        float runspan = 0;

        [SerializeField]
        AudioClip[] jumpSound;
        [SerializeField]
        AudioClip deathSound;
        public deathfade Dfade;
        public fadein fade;
        public BosController boscon;
        public float playerHP = 100;
        public float playerMP = 100;
        public float MaxMP = 100;
        public float MaxHP = 100;

        public float damage;
        KeywordRecognizer keywordRecognizer;
        Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
        public GameObject redbullet;
        public GameObject air;
        public GameObject frieze;
        public GameObject magicring;
        private GameObject magicAttackparticle;
        private GameObject ringparticle;

        private GameObject chargeparticle;
        bool ringcount = false;
        public mobController mobController1;
        public mobController mobController4;
        public GameObject player;
        Vector3 playerpos = new Vector3(0f, 0f, 0f);
        public float deleteTime = 3.0f;
        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;
        bool isJumping = false;
        public bool alive = true;
        public bool mgcharge = false;

        private bool enarea = false;
        private bool entouch = false;
        private bool heat = false;
        float enemypos = 0f;
        private Vector2 hitV;
        bool health = false;
        public float MPHealthscore = 1;
        public bool warpOK = false;
        private bool BosAttackjudge = false;
        private bool delay = false;
        private bool water = false;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            fade.isFadeIn = true;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            hitV = new Vector2(0, 0);
            //反応するキーワードを辞書に登録
            keywords.Add("ふぁいや", () =>
            {
                Debug.Log("「ふぁいや」をキーワードに指定");
            });
            keywords.Add("ふぁえや", () =>
            {
                Debug.Log("「ふぁえや」をキーワードに指定");
            });
            keywords.Add("ふぁいあ", () =>
            {
                Debug.Log("「ふぁいあ」をキーワードに指定");
            });
            keywords.Add("ふりーず", () =>
            {
                Debug.Log("「ふりーず」をキーワードに指定");
            });
            keywords.Add("えあ", () =>
            {
                Debug.Log("「えあ」をキーワードに指定");
            });





            //キーワードを渡す
            keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

            //キーワードを認識したら反応するOnPhraseRecognizedに「KeywordRecognizer_OnPhraseRecognized」処理を渡す
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

            //音声認識開始
            keywordRecognizer.Start();
            Debug.Log("音声認識開始");
        }

        private void Update()
        {
            playerpos = player.transform.position;
            Destroy(magicAttackparticle, deleteTime);
            if (alive)
            {

                Hurt();
                Die();
                if (!anim.GetBool("isJump"))
                    Attack();
                if (!mgcharge && !delay)
                {
                    Jump();
                    Run();
                }
            }

        }
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.tag == "Enemy" && !enarea)
            {
                entouch = true;
                enemypos = other.transform.position.x;
                float Pm = playerpos.x - enemypos;
                if (Pm > 0)
                    direction = -1;
                else if (Pm <= 0)
                    direction = 1;
                damage = 1;
                Debug.Log("touch " + damage);
            }
            if (other.transform.tag == "ground")
            {
                anim.SetBool("isJump", false);
            }
            if (other.transform.tag == "water")
            {
                water = true;
            }

        }
        void OnTriggerStay2D(Collider2D other)
        {
            if (!entouch && other.transform.tag == "Enemyarea1" || other.transform.tag == "Enemyarea4")
            {
                enarea = true;
                enemypos = other.transform.position.x;
                float Pm = playerpos.x - enemypos;
                if (Pm > 0)
                    direction = -1;
                else if (Pm <= 0)
                    direction = 1;
                if (other.transform.tag == "Enemyarea1")
                {
                    damage = mobController1.attackPower;
                }
                else if (other.transform.tag == "Enemyarea4")
                {
                    damage = mobController4.attackPower;
                }
                Debug.Log("area " + damage);
            }
        }
        void OnParticleCollision(GameObject other)
        {
            if (other.transform.tag == "BosAttack")
            {
                BosAttackjudge = true;
                damage = boscon.attackPower;
            }
        }
        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                {
                    anim.SetBool("isRun", true);
                    runspan -= Time.deltaTime;
                    if (runspan <= 0)
                    {
                        audioSource.PlayOneShot(runSound[Random.Range(0, runSound.Length)]);
                        runspan = 0.3f; //タイマーを0.5秒に戻す
                    }
                }
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                {
                    anim.SetBool("isRun", true);
                    runspan -= Time.deltaTime;
                    if (runspan <= 0)
                    {
                        audioSource.PlayOneShot(runSound[Random.Range(0, runSound.Length)]);
                        runspan = 0.3f; //タイマーを0.5秒に戻す
                    }
                }

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        void Jump()
        {
            if (!heat && (Input.GetButtonDown("Jump"))
            && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
                audioSource.PlayOneShot(jumpSound[Random.Range(0, jumpSound.Length)]);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }
        void Attack()
        {
            anim.SetBool("isRun", false);
            if (Input.GetKey("r") && playerMP > 0)
            {
                if (MaxMP > playerMP && !health)
                {
                    health = true;
                    Invoke("MPHealth", 1);
                }
                if (!ringcount)
                {
                    ringparticle = Instantiate(magicring, playerpos, Quaternion.identity);
                    var particleParticle = ringparticle.GetComponent<ParticleSystem>();
                    particleParticle.transform.Rotate(-90f, 0f, 0f);
                    // 表示
                    particleParticle.Play();
                    ringcount = true;
                }
                mgcharge = true;
            }
            if (Input.GetKeyUp("r"))
            {
                mgcharge = false;
                Destroy(ringparticle);
                ringcount = false;
            }
        }
        private void MPHealth() //コルーチン関数の名前
        {
            playerMP += MPHealthscore;
            health = false;
        }
        void Hurt()
        {
            if (!heat)
            {
                if (enarea || entouch || BosAttackjudge)
                {
                    heat = true;
                    anim.SetTrigger("hurt");
                    if (direction == 1)
                        rb.AddForce(hitV = new Vector2(-10f, 8f), ForceMode2D.Impulse);
                    else
                        rb.AddForce(hitV = new Vector2(10f, 8f), ForceMode2D.Impulse);
                    playerHP -= damage;
                }
            }
            else if (heat)
            {
                Invoke("Reh", 1);
            }
            entouch = false;
            enarea = false;
            BosAttackjudge = false;
            damage = 0;
        }
        void Reh()
        {
            heat = false;
        }
        public GameObject restart;
        public GameObject exit;
        public GameObject gameOver;
        void Die()
        {
            if (playerHP <= 0 || water || playerpos.y <= -10)
            {
                Destroy(bgm);
                water = false;
                audioSource.PlayOneShot(deathSound);
                anim.SetTrigger("die");
                alive = false;
                Dfade.isdeathFadeOut = true;
                restart.gameObject.SetActive(true);
                exit.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(true);
            }
        }
        public void Redbullet()
        {
            delay = true;
            Invoke("DelayTime", 0.5f);
            playerMP -= 10;
            // prefabをロード。Assets直下ではなくAssets/Resourcesからの相対パスを入れる。
            // プレハブからインスタンスを生成。
            magicAttackparticle = Instantiate(redbullet, playerpos + new Vector3(2 * direction, 3, 0), Quaternion.identity);
            // ParticleSystemを取得
            var particleParticle = magicAttackparticle.GetComponent<ParticleSystem>();
            particleParticle.transform.Rotate(0f, 90f * direction, 0f);
            // 表示
            particleParticle.Play();
            audioSource.PlayOneShot(magicSound[0]);
        }
        public void Frieze()
        {
            delay = true;
            Invoke("DelayTime", 1f);
            playerMP -= 5;
            // prefabをロード。Assets直下ではなくAssets/Resourcesからの相対パスを入れる。
            // プレハブからインスタンスを生成。
            magicAttackparticle = Instantiate(frieze, playerpos + new Vector3(2 * direction, 3, 0), Quaternion.identity);
            // ParticleSystemを取得
            var particleParticle = magicAttackparticle.GetComponent<ParticleSystem>();
            particleParticle.transform.Rotate(20f, 90f * direction, 0f);
            // 表示
            particleParticle.Play();
            audioSource.PlayOneShot(magicSound[1]);
        }
        public void Air()
        {
            delay = true;
            Invoke("DelayTime", 2f);
            playerMP -= 30;
            // prefabをロード。Assets直下ではなくAssets/Resourcesからの相対パスを入れる。
            // プレハブからインスタンスを生成。
            magicAttackparticle = Instantiate(air, playerpos + new Vector3(5 * direction, 5, 0), Quaternion.identity);
            // ParticleSystemを取得
            var particleParticle = magicAttackparticle.GetComponent<ParticleSystem>();
            if (direction == 1)
                particleParticle.transform.Rotate(0f, 0f, 270f);
            if (direction == -1)
                particleParticle.transform.Rotate(0f, 180f, 270f);
            // 表示
            particleParticle.Play();
            audioSource.PlayOneShot(magicSound[2]);
        }
        public void DelayTime()
        {
            delay = false;
        }

        private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {

            //デリゲート
            //イベントやコールバック処理の記述をシンプルにしてくれる。クラス・ライブラリを活用するには必須らしい
            System.Action keywordAction;//keywordActionという処理を行う
                                        //認識されたキーワードが辞書に含まれている場合に、アクションを呼び出す。
            if (mgcharge && keywords.TryGetValue(args.text, out keywordAction))
            {
                if (args.text == keywords.Keys.ToArray()[0] || args.text == keywords.Keys.ToArray()[1] || args.text == keywords.Keys.ToArray()[2])
                {
                    anim.SetTrigger("attack");
                    Redbullet();
                }
                if (args.text == keywords.Keys.ToArray()[3])
                {
                    anim.SetTrigger("attack");
                    Frieze();
                }
                if (args.text == keywords.Keys.ToArray()[4])
                {
                    anim.SetTrigger("attack");
                    Air();
                }

            }
        }

    }

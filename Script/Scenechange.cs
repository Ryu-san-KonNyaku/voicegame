using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Scenechange : Button
{
    public GameObject ExitButton;
    // Start is called before the first frame update
    public Animator startanim;
    public GameObject player;
    private bool startok = false;
    public fadeout fade;
    [SerializeField]
    AudioClip[] run;
    float span = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startanim = player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (startok)
        {
            player.transform.position += Vector3.right * 10 * Time.deltaTime;
            span -= Time.deltaTime;
            if (span <= 0)
            {
                audioSource.PlayOneShot(run[Random.Range(0, run.Length)]);
                span = 0.3f; //タイマーを0.5秒に戻す
            }
        }
    }
    private IEnumerator Spawn()
    {
        // 3回繰り返す
        while (true)
        {
            // 食べ物を生成
            audioSource.PlayOneShot(run[Random.Range(0, run.Length)]);
            // 1秒待つ
            yield return new WaitForSeconds(1f);
        }
    }
    public void OnClickStartButton()
    {
        audioSource.PlayOneShot(button);
        fade.isFadeOut = true;
        Invoke("GameStart", 3);
        this.GetComponent<Image>().enabled = false;
        ExitButton.GetComponent<Image>().enabled = false;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        ExitButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        startok = true;
        startanim.SetBool("isRun", true);
    }
    void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

}

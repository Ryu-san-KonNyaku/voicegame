using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClear : MonoBehaviour
{
    public SimplePlayerController player;
    public AudioSource audioSource;
    public int count = 0;
    public GameObject bos;
    public GameObject bgm;
    [SerializeField]
    AudioClip clearSound;
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    [SerializeField]
    AudioClip brakeobject;
    int timecount;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // clear = playertext.GetComponent<Text>();

        if (timecount < count)
        {
            audioSource.PlayOneShot(brakeobject);

        }
        if (count == 3)
        {
            Invoke("Death", 3f);
            count++;
        }
        timecount = count;
    }
    void Death()
    {
        Destroy(bos);
        Destroy(bgm);
        audioSource.PlayOneShot(clearSound);
        player.alive = false;
        player.Dfade.isdeathFadeOut = true;
        player.restart.gameObject.SetActive(true);
        player.exit.gameObject.SetActive(true);
        player.gameOver.gameObject.SetActive(true);
        cardNameText.text = "ゲームクリア!!";

    }
}

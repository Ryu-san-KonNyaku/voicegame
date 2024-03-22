using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ReStart : Button
{
    public GameObject ExitButton;
    // Start is called before the first frame update
    public bool restart;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    public void OnClickStartButton()
    {
        audioSource.PlayOneShot(button);
        Invoke("GameStart", 3);
        this.GetComponent<Image>().enabled = false;
        ExitButton.GetComponent<Image>().enabled = false;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        ExitButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        restart = true;
    }
    void GameStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}

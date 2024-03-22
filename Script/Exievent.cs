using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exievent : Button
{
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    public void ButtonExit()
    {
        audioSource.PlayOneShot(button);
        Application.Quit();
    }
}

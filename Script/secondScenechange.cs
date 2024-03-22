using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class secondScenechange : MonoBehaviour
{
    public fadeout fade;
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            fade.isFadeOut = true;
            Invoke("BosStart", 3);
        }

    }
    void BosStart()
    {
        SceneManager.LoadScene("BosScene");
    }
}

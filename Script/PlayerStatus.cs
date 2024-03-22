using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    public SimplePlayerController status;
    GameObject Health;
    GameObject Mana;
    Image HPbar;
    Image Manabar;

    // Start is called before the first frame update
    void Start()
    {
        //GetChildren(this.gameObject);
        Health = transform.GetChild(0).gameObject;
        Mana = transform.GetChild(1).gameObject;
        HPbar = Health.GetComponent<Image>();
        Manabar = Mana.GetComponent<Image>();

    }
    // Update is called once per frame
    void Update()
    {
        HPbar.fillAmount = status.playerHP / 100.0f;
        Manabar.fillAmount = status.playerMP / 100.0f;
    }

}

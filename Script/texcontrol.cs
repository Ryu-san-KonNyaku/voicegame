using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class texcontrol : MonoBehaviour
{
    public GameClear clear;
    [SerializeField]
    private TextMeshProUGUI cardNameText;

    // Update is called once per frame
    void Update()
    {
        if (clear.count == 3)
        {
            cardNameText.text = "ゲームクリア";
        }
    }
}

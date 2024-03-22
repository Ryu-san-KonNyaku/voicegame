using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
public class deathfade : MonoBehaviour
{
    float fadeSpeed = 0.005f;        //透明度が変わるスピードを管理
    float red, green, blue, alfa;   //パネルの色、不透明度を管理

    public bool isdeathFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ

    Image fadeImage;                //透明度を変更するパネルのイメージ
    public ReStart res;
    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    void Update()
    {
        if (isdeathFadeOut)
        {
            StartFadeOut();
        }
        if (res.restart)
        {
            Allfadeout();
        }
    }
    void Allfadeout()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            res.restart = false;
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 0.7)
        {             // d)完全に不透明になったら処理を抜ける
            isdeathFadeOut = false;
        }

    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}

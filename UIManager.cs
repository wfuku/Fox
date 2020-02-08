using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public Image hpGauge,hpGauge2,epGauge;
    public Image sunny, rainy, cloudy,biyaku,tamago,tane;
    public float playerHp, playerEp;
    MinoriState playerStatus;
    Color epColor;
    public string condition,weather;




    // Start is called before the first frame update
    void Start()
    {
        playerStatus = player.GetComponent<MinoriState>();//プレイヤーのステータスを取得
        epColor = epGauge.color;//初期のＥｐゲージカラーを取得しておく


    }

    // Update is called once per frame
    void Update()
    {
        Gauge();//ゲージの管理
        Weather();//天気の管理
        Condition();//キャラ状態異常の管理


    }

    void Gauge() {
        playerHp = playerStatus.hp;
        playerEp = playerStatus.ep;
        epGauge.color = epColor;//epゲージのカラー取得
        hpGauge.fillAmount = playerHp / 100f;//HPゲージ

        //赤ｈｐに合わせ黄色ｈｐゲージを徐々に減らす
        if (hpGauge != hpGauge2)
        {

            if (hpGauge.fillAmount > hpGauge2.fillAmount)
            {
                hpGauge2.fillAmount += 0.01f;

            }
            else { hpGauge2.fillAmount -= 0.01f; }
        }
        //epに合わせてゲージの色を変える
        epColor.a = playerEp / 100;
    }

    //天気ＵＩの切り替え
    void Weather()
    {
        weather = playerStatus.weahter;

        sunny.enabled = false;
        rainy.enabled = false;
        cloudy.enabled = false;

        switch (weather)
        {
            case "SUNNY":
                sunny.enabled = true;
                break;

            case "CLOUDY":
                cloudy.enabled = true;
                break;

            case "RAINY":
                rainy.enabled = true;
                break;

            default:
                break;
        }
    }

    //状態異常ＵＩの切り替え
    void Condition()
    {
        biyaku.enabled = playerStatus.biyaku;
        tane.enabled = playerStatus.tane;
        tamago.enabled = playerStatus.tamago;
    }
}

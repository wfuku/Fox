using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEffect : MonoBehaviour
{
    public float alfa,speed = 0.01f,waitTime,red,green,blue;
    public bool startOut = false,startIn = false;
    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }

    //ブラックアウト,イン
    public void BlackOut(float changeTime, float outSpeed)
    {
        if (startOut == false && startIn == false)
        {
            red = 0;
            green = 0;
            blue = 0;
            alfa = 0;
            waitTime = 0;
        }

        if (alfa <= 1 && startIn == false)
        {
            alfa += outSpeed;
            startOut = true;
        }else if (waitTime<=changeTime)
        {
            waitTime += Time.deltaTime;
        }
        else if(alfa >=0)
        {
            startIn = true;
            alfa -= outSpeed;
        }

    }

}

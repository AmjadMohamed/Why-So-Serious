using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float Timer = 0;
    public Text PixelatedText;


    void Update()
    {
        StartTimer();        
    }

    void StartTimer()
    {
        Timer = Time.time;
        PixelatedText.text = Mathf.Floor(Timer / 60).ToString("00") + ":" + Mathf.FloorToInt(Timer % 60).ToString("00");
    }
}

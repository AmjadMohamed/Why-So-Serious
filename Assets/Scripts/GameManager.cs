using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float Timer = 0;
    public Text TimerText;
    public Text BoxCounterText;
    public Text CloneCounterText;


    public GameObject Shaco;
    


    void Update()
    {
        StartTimer();
        BoxCounterText.text = Shaco.GetComponent<Player>().BoxCounter.ToString() + "*";
        CloneCounterText.text = Shaco.GetComponent<Player>().CloneCounter.ToString() + "*";
    }

    void StartTimer()
    {
        Timer = Time.time;
        TimerText.text = Mathf.Floor(Timer / 60).ToString("00") + ":" + Mathf.FloorToInt(Timer % 60).ToString("00");
    }
    
}

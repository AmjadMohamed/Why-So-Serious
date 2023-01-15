using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // public
    [HideInInspector] public int PressedBoxesCounter = 0;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI BoxCounterText;
    [SerializeField] TextMeshProUGUI CloneCounterText;
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] TextMeshProUGUI HighestScore;
    public TextMeshProUGUI LoseCause;
    [SerializeField] GameObject Shaco;
    [SerializeField] GameObject LockedDoor;
    [SerializeField] GameObject OpenedDoor;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject TutorialPanel;

    // private
    float Timer = 0;
    private const string HighestScorePlayerPrefKey = "HighestScore";


    private void Awake()
    {
        if (!Instance)
            Instance = this;

        if (TutorialPanel.active == true)
        {
            Time.timeScale = 0;
        }
    }


    void Update()
    {
        //print("pressedBoxes: " +PressedBoxesCounter);
        StartTimer();
        if (Shaco != null)
        {
            BoxCounterText.text = Shaco.GetComponent<Shaco_Script>().BoxCounter.ToString() + "*";
            CloneCounterText.text = Shaco.GetComponent<Shaco_Script>().CloneCounter.ToString() + "*";
            // lose state
            if (Shaco.GetComponent<Shaco_Script>().BoxCounter < 1 && Shaco.GetComponent<Shaco_Script>().CloneCounter < 1 && PressedBoxesCounter < 3)
            {
                LostGame();
                LoseCause.text = "You Lost Because You Ran Out Of Solutions!!";
            }
        }

        if (PressedBoxesCounter == 3)
        {
            LockedDoor.SetActive(false);
            OpenedDoor.SetActive(true);
        }
        else
        {
            LockedDoor.SetActive(true);
            OpenedDoor.SetActive(false);
        }
    }

    void StartTimer()
    {
        Timer = Time.timeSinceLevelLoad;
        TimerText.text = Mathf.Floor(Timer / 60).ToString("00") + ":" + Mathf.FloorToInt(Timer % 60).ToString("00");
    }

    public void WonGame()
    {
        Time.timeScale = 0;
        if (PlayerPrefs.HasKey(HighestScorePlayerPrefKey))
        {
            // updating new highest score
            if (Timer < PlayerPrefs.GetFloat(HighestScorePlayerPrefKey))
            {
                PlayerPrefs.SetFloat(HighestScorePlayerPrefKey, Timer);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(HighestScorePlayerPrefKey, Timer);
        }
        Score.text = $"Your Score is: {Mathf.Floor(Timer / 60).ToString("00")} : {Mathf.FloorToInt(Timer % 60).ToString("00")}";
        HighestScore.text = $"Highest Score is: {Mathf.Floor(PlayerPrefs.GetFloat(HighestScorePlayerPrefKey) / 60).ToString("00")} : {Mathf.FloorToInt(PlayerPrefs.GetFloat(HighestScorePlayerPrefKey) % 60).ToString("00")}";
        WinPanel.SetActive(true);
    }

    public void LostGame()
    {
        Time.timeScale = 0;
        LosePanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        TutorialPanel.SetActive(false);
    }
}

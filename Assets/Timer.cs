//using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;
    [SerializeField] private Text timeDisplay;
    [SerializeField] private Text Gamebegin;
    GameObject playerObj;
    private int Countdown;
    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        currentTime = maxTime;
        Gamebegin.text = "";
        timeDisplay.text = maxTime.ToString();
        Countdown = 3;
    }
    private void Update()
    {
        SimpleSampleCharacterControl control = playerObj.GetComponentInChildren<SimpleSampleCharacterControl>();
        if (control.PAUSE == 1) return;

        if (timeDisplay.text == "0:00")
        {
            Gamebegin.text = "Time UP!";
            SceneManager.LoadScene("GameOver");
        }
            else
            {
                currentTime = currentTime - Time.deltaTime;
                int m = (((int)currentTime) % 60);
            string timeText;
            if (m < 10)
            {
               timeText = (((int)currentTime) / 60).ToString() + ":0" + m.ToString();
            }
            else
            {
               timeText = (((int)currentTime) / 60).ToString() + ":" + m.ToString();
            }                 
            timeDisplay.text = timeText;
            }
     }
}

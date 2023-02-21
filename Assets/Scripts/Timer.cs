using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Timer;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timertext;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countdown;

    [Header("Limit Settings")]
    // timLim = timer Limit - this is set to put a limit on the timer
    public bool timLim;
    public float timerLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countdown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        if(timLim  && ((countdown && currentTime <= timerLimit) || (!countdown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            timertext.color = Color.red;
            enabled = false;
        }
        timertext.text = currentTime.ToString("0.00");
    }
}

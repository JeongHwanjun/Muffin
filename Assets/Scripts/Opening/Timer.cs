using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int openingMin = 3;
    public int openingSec = 0;
    private float openingTime;

    public event Action OnTimeout;
    public event Action OnTimeLow;
    private bool sentLowAlarm = false;

    void Start()
    {
        openingTime = openingMin * 60f + openingSec;
    }

    void Update()
    {
        openingTime -= Time.deltaTime;
        if (openingTime < 0f)
        {
            openingTime = 0f;
            Time.timeScale = 0f;
            // 시간종료!!!
            Debug.Log("Timer : Timeout!");
            OnTimeout?.Invoke();
        }
        else if (openingTime <= 10 && !sentLowAlarm)
        {
            Debug.Log("Timer : TimeLow");
            OnTimeLow?.Invoke();
            sentLowAlarm = true;
        }
    }
}

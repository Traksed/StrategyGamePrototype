using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public string Name { get; private set; }
    
    public bool IsRunning { get; private set; }
    private DateTime _startTime;
    public TimeSpan TimeToFinish { get; private set; }
    private DateTime _finishTime;
    public UnityEvent TimerFinishedEvent;

    public double SecodsLeft { get; private set; }

    public int SkipAmount
    {
        get
        {
            return (int)(SecodsLeft / 60) * 2;
        }
    }

    public void Initialize(string processName, DateTime start, TimeSpan time)
    {
        Name = processName;

        _startTime = start;
        TimeToFinish = time;
        _finishTime = start.Add(time);

        TimerFinishedEvent = new UnityEvent();
    }

    public void StartTimer()
    {
        SecodsLeft = TimeToFinish.TotalSeconds;
        IsRunning = true;
    }

    private void Update()
    {
        if (IsRunning)
        {
            if (SecodsLeft > 0)
            {
                SecodsLeft -= Time.deltaTime;
            }
            else
            {
                SecodsLeft = 0;
                IsRunning = false;
                TimerFinishedEvent.Invoke();
            }
        }
    }

    public string DisplayTime()
    {
        string text = "";
        TimeSpan timeLeft = TimeSpan.FromSeconds(SecodsLeft);

        if (timeLeft.Days != 0)
        {
            text += timeLeft.Days + "d";
            text += timeLeft.Hours + "h";
        }
        else if (timeLeft.Hours != 0)
        {
            text += timeLeft.Hours + "h";
            text += timeLeft.Minutes + "m";
        }
        else if (timeLeft.Minutes != 0)
        {
            text += timeLeft.Minutes + "m";
            text += timeLeft.Seconds + "s";
        }
        else if (SecodsLeft > 0)
        {
            text += Mathf.FloorToInt((float)SecodsLeft) + "sec";
        }
        else
        {
            text = "Finished";
        }

        return text;
    }

    public void SkipTimer()
    {
        SecodsLeft = 0;
        _finishTime = DateTime.Now;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float endTime;

    public bool isDone {
        get {
            return (endTime <= Time.time);
        }
    }

    public float timeLeft {
        get {
            return Mathf.Max(endTime - Time.time, 0);
        }
    }

    public Timer() {
        endTime = 0;
    }

    public void SetTime(float time) {
        endTime = Time.time + time;
    }
}
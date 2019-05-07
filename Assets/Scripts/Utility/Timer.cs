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

    public Timer() {
        endTime = 0;
    }

    public void SetTime(float time) {
        endTime = Time.time + time;
    }

    public float GetTime() {
        return Mathf.Max(endTime - Time.time, 0);
    }
}
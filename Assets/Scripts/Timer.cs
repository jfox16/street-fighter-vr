using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom Timer implementation
public class Timer {

    float endTime;

    // Returns true if the Timer is at 0, false otherwise.
    public bool isDone {
        get {
            return (endTime <= Time.time);
        }
        private set {}
    }

    // Returns the time left until the Timer is done.
    public float timeLeft {
        get {
            return (Mathf.Max(endTime - Time.time, 0));
        }
        private set {}
    }



    // Constructor
    public Timer() {
        endTime = 0;
    }
    // Constructor with initialized time
    public Timer(float time) {
        SetTime(time);
    }



    // Sets the Timer
    public void SetTime(float time) {
        endTime = Time.time + time;
    }

    // Adds time to the Timer
    public void AddTime(float time) {
        endTime += time;
    }
}
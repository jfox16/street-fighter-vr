﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float seconds, maxTime;
    public GameObject myText, WinOrLose;
    private Text text;
    private bool carDead;
    Color trans, red;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        maxTime = 10;
        carDead = false;

        trans = new Color(0.0f, 0.0f, 1.0f, 0.0f);
        red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        text = WinOrLose.gameObject.GetComponent<Text>();
        text.color = trans;
        text.text = "Nice Try!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!carDead && seconds < maxTime)
        {
            seconds += Time.deltaTime;
        }
        else if(!carDead && seconds >= maxTime)
        {
            text.color = Color.Lerp(text.color, red, 2.0f * Time.deltaTime);
        }
        myText.GetComponent<Text>().text = ((int)(maxTime - seconds)).ToString();
    }

    public void setCarDead(bool carDead)
    {
        this.carDead = carDead;
    }
}

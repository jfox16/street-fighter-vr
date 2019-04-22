using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float seconds, maxTime;
    public GameObject myText, WinOrLose, score, car, carParts;
    private Text text;
    private bool carDead;
    private Destruction s;
    private CarPartsHealth s1;
    Color trans, red;
    // Start is called before the first frame update
    void Start()
    {
    
        s = car.gameObject.GetComponent<Destruction>();
        s1 = carParts.GetComponent<CarPartsHealth>();

        seconds = 0;
        maxTime = 180;
        carDead = false;

        trans = new Color(0.0f, 0.0f, 1.0f, 0.0f);
        red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        text = WinOrLose.gameObject.GetComponent<Text>();
        text.color = trans;
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
            text.text = "Nice Try!";
            text.color = Color.Lerp(text.color, red, 2.0f * Time.deltaTime);
            int playerScore = s.getScore();
            score.GetComponent<Text>().text = "Player Score: " + playerScore.ToString();
            s.enabled = false;
            s1.enabled = false;
        }
        myText.GetComponent<Text>().text = ((int)(maxTime - seconds)).ToString();
    }

    public void setCarDead(bool carDead)
    {
        this.carDead = carDead;
    }

    public int getSecondsLeft()
    {
        return (int)(maxTime - seconds);
    }


}

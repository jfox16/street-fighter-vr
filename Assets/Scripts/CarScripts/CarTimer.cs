using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTimer : MonoBehaviour
{
    private float seconds, maxTime;
    public GameObject myText, WinOrLose, score, car, carParts;
    private GameObject soundPlayer;
    private TextMesh text;
    private bool carDead, loserPlayed;
    private Destruction s;
    Color trans, red;
    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.Find("SoundPlayer");

        s = car.gameObject.GetComponent<Destruction>();

        seconds = 0;
        maxTime = 30;
        carDead = false;

        trans = new Color(0.0f, 0.0f, 1.0f, 0.0f);
        red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        text = WinOrLose.gameObject.GetComponent<TextMesh>();
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
            score.GetComponent<TextMesh>().text = "Player Score: " + playerScore.ToString();
            s.enabled = false;
            if (!loserPlayed)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/Loser", soundPlayer.transform.position);
                loserPlayed = true;
            }
        }
        myText.GetComponent<TextMesh>().text = ((int)(maxTime - seconds)).ToString();
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

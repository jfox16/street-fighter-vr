using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{

    public GameObject HiScore1, HiScore2, HiScore3, HiScore4, HiScore5;
    public Destruction d;
    public Timer t;
    private int[] scores;
    private bool Checked;

    // Start is called before the first frame update
    void Start()
    {
        // Just for resetting the scoreboard
      /*PlayerPrefs.DeleteKey("FirstPlace");
        PlayerPrefs.DeleteKey("SecondPlace");
        PlayerPrefs.DeleteKey("ThirdPlace");
        PlayerPrefs.DeleteKey("ForthPlace");
        PlayerPrefs.DeleteKey("FifthPlace");*/
        scores = new int[5]{PlayerPrefs.GetInt("FirstPlace", 0), PlayerPrefs.GetInt("SecondPlace", 0), PlayerPrefs.GetInt("ThirdPlace", 0), PlayerPrefs.GetInt("ForthPlace", 0), PlayerPrefs.GetInt("FifthPlace", 0)};
        HiScore1.GetComponent<Text>().text = "#1: " + scores[0];
        HiScore2.GetComponent<Text>().text = "#2: " + scores[1];
        HiScore3.GetComponent<Text>().text = "#3: " + scores[2];
        HiScore4.GetComponent<Text>().text = "#4: " + scores[3];
        HiScore5.GetComponent<Text>().text = "#5: " + scores[4];
        Checked = false;
    }

    // Update is called once per frame
    void Update()
    {
        int playerScore = d.getScore();
        int fifthScore = scores[4];
        if ((d.getHealth() <= 0 || t.getSecondsLeft() <= 0) && playerScore >= fifthScore && !Checked)
        {
            checkScores(playerScore);
            Checked = true;
        }
    }

    private void checkScores(int playerScore)
    {
        bool inserted = false;
        int[] temp = new int[5];
        int j = 0;
        for(int i = 0; i < scores.Length - 1; i++)
        {
            if(playerScore >= scores[i] && !inserted)
            {
                temp[j] = playerScore;
                temp[++j] = scores[i];
                inserted = true;
            }
            else
            {
                temp[j] = scores[i];
            }
            j++;
        }
        scores = temp;
        PlayerPrefs.SetInt("FirstPlace", scores[0]);
        PlayerPrefs.SetInt("SecondPlace", scores[1]);
        PlayerPrefs.SetInt("ThirdPlace", scores[2]);
        PlayerPrefs.SetInt("ForthPlace", scores[3]);
        PlayerPrefs.SetInt("FifthPlace", scores[4]);
        Array.Clear(temp, 0, temp.Length);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destruction : MonoBehaviour
{
    private int playerScore;
    private float carHealth;
    private GameObject g, g1;
    public GameObject scoreText, WinOrLose;
    private Text text;
    private CarParts s;
    private Timer timer;
    private GameObject test;
    private bool brokenAlready;
    Color trans, blue;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("CarParts");
        g1 = GameObject.Find("Game Controller");
        carHealth = 300.0f;
        s = g.gameObject.GetComponent<CarParts>();
        timer = g1.gameObject.GetComponent<Timer>();
        playerScore = 0;
        brokenAlready = false;

        trans = new Color(0.0f, 0.0f, 1.0f, 0.0f);
        blue = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        text = WinOrLose.gameObject.GetComponent<Text>();
        text.color = trans;
        text.text = "Winner Winner Chicken Dinner!";

    }

    private void Update()
    {
        if(carHealth <= 0.0f && !brokenAlready)
        {
            //Debug.Log(s.getCarParts().Count);
            breakAllParts();
            brokenAlready = true;
        }
        else if (brokenAlready)
        {
            text.color = Color.Lerp(text.color, blue, 2.0f * Time.deltaTime);
        }
        scoreText.GetComponent<Text>().text = "Player Score: " + playerScore.ToString();
    }

    void breakAllParts()
    {
        foreach(GameObject item in s.getCarParts().Values)
        {
            if(item.GetComponent<CarPartsHealth>() != null)
            {
                item.GetComponent<CarPartsHealth>().setBroken(true);
            }
            if (item.GetComponent<Rigidbody>() == null)
            {
                Rigidbody t = item.gameObject.AddComponent<Rigidbody>();
                t.mass = 0.1f;
                float randomDir = Random.Range(-1.0f, 1.0f);
                t.AddForce((item.transform.forward + item.transform.up) * 3.0f * randomDir);
            }
        }
        timer.setCarDead(true);
        playerScore += 500;
    }

    public float getHealth()
    {
        return carHealth;
    }

    public void setHealth(float health)
    {
        this.carHealth = health;
    }

    public int getScore()
    {
        return playerScore;
    }

    public void setScore(int score)
    {
        this.playerScore = score;
    }


}

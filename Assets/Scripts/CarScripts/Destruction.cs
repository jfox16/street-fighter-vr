using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destruction : MonoBehaviour
{
    private int playerScore;
    private float carHealth;
    private GameObject g, soundPlayer;
    public GameObject scoreText, WinOrLose, cheer, explosion, smoke;
    private TextMesh text;
    private CarParts s;
    private CarTimer timer;
    private GameObject test;
    private bool brokenAlready;
    Color trans, blue;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("CarParts");
        // g1 = GameObject.Find("Game Controller");
        soundPlayer = GameObject.Find("SoundPlayer");
        carHealth = 30.0f;
        s = g.gameObject.GetComponent<CarParts>();
        timer = GetComponent<CarTimer>();
        playerScore = 0;
        brokenAlready = false;
        trans = new Color(0.0f, 0.0f, 1.0f, 0.0f);
        blue = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        text = WinOrLose.gameObject.GetComponent<TextMesh>();
        text.color = trans;
        text.text = "Winner Winner!";

    }

    private void Update()
    {
        Debug.Log(carHealth);
        if(carHealth <= 0.0f && !brokenAlready)
        {
            breakAllParts();
            brokenAlready = true;
        }
        else if (brokenAlready)
        {
            text.color = Color.Lerp(text.color, blue, 2.0f * Time.deltaTime);
        }
    }

    void breakAllParts()
    {
        foreach(GameObject item in s.getCarParts().Values)
        {
            item.GetComponent<CarPartsHealth>().setBroken(true);
            if (item.GetComponent<Rigidbody>().isKinematic)
            {
                Rigidbody t = item.gameObject.GetComponent<Rigidbody>();
                t.isKinematic = false;
                t.mass = 0.1f;
                float randomDir = Random.Range(-1.0f, 1.0f);
                t.AddForce((item.transform.forward + item.transform.up) * 3.0f * randomDir);
            }
        }
        timer.setCarDead(true);
        int victoryPoints = 150 + (timer.getSecondsLeft() * 2);
        playerScore += victoryPoints;
        Instantiate(explosion, this.transform.position + new Vector3(0,2,0), new Quaternion(0, 0, 0, 0));
        StartCoroutine(SpawnSmoke());
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/Winner", soundPlayer.transform.position);
        cheer.SetActive(true);
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

    IEnumerator SpawnSmoke()
    {
        yield return new WaitForSeconds(2);
        smoke.SetActive(true);
    }


}

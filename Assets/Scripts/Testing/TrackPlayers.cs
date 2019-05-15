using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayers : MonoBehaviour
{
    public GameObject timer1, timer2;
    private float seconds, maxTime;
    private GameObject soundPlayer, playerOne, playerTwo;
    private GameObject[] players;
    private Fighter p1, p2;
    private PlayerStatus s1, s2;
    private bool playTimesUp, playKO;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.Find("Sound Player");
        seconds = 0;
        maxTime = 20;
        playTimesUp = false;
        playKO = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(players == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            // will probably have to change this when implementing to the network
            playerOne = players[1];
            playerTwo = players[2];

            playerOne.gameObject.GetComponent<PlayerStatus>().enabled = true;
            playerTwo.gameObject.GetComponent<PlayerStatus>().enabled = true;

            p1 = playerOne.gameObject.GetComponent<Fighter>();
            p2 = playerTwo.gameObject.GetComponent<Fighter>();
            s1 = playerOne.gameObject.GetComponent<PlayerStatus>();
            s2 = playerTwo.gameObject.GetComponent<PlayerStatus>();
        }
        if (p1.getHealth() > 0 && p2.getHealth() > 0 && seconds < maxTime)
        {
            seconds += Time.deltaTime;
        }
        // when time ran out but no player lost all their health
        else if (seconds >= maxTime)
        {
            if (!playTimesUp)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/TimeUp", soundPlayer.gameObject.transform.position);
                playTimesUp = true;
            }

            // in case of a tie
            if (p1.getHealth() == p2.getHealth())
            {
                s1.SetGameStatus(PlayerStatus.GameStatus.Tie);
                s2.SetGameStatus(PlayerStatus.GameStatus.Tie);
            }
            // player 1 wins
            else if (p1.getHealth() > p2.getHealth())
            {
                s1.SetGameStatus(PlayerStatus.GameStatus.Winner);
                s2.SetGameStatus(PlayerStatus.GameStatus.Loser);
            }
            // player 2 wins
            else if (p1.getHealth() < p2.getHealth())
            {
                s1.SetGameStatus(PlayerStatus.GameStatus.Loser);
                s2.SetGameStatus(PlayerStatus.GameStatus.Winner);
            }
        }

        // when a player lost all their health
        if((p1.getHealth() <= 0 || p2.getHealth() <= 0) && !playKO && seconds < maxTime)
        {
            StartCoroutine(KOState());
        }
        timer1.GetComponent<TextMesh>().text = "Time Left: " + ((int)(maxTime - seconds)).ToString();
        timer2.GetComponent<TextMesh>().text = "Time Left: " + ((int)(maxTime - seconds)).ToString();
    }

    IEnumerator KOState()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/KO", soundPlayer.gameObject.transform.position);
        playKO = true;
        yield return new WaitForSeconds(1.5f);
        // in case of a tie
        if (p1.getHealth() <= 0 && p2.getHealth() <= 0)
        {
            s1.SetGameStatus(PlayerStatus.GameStatus.Tie);
            s2.SetGameStatus(PlayerStatus.GameStatus.Tie);
        }
        // player 1 wins
        else if (p1.getHealth() > 0 && p2.getHealth() <= 0)
        {
            s1.SetGameStatus(PlayerStatus.GameStatus.Winner);
            s2.SetGameStatus(PlayerStatus.GameStatus.Loser);
        }
        // player 2 wins
        else if (p1.getHealth() <= 0 && p2.getHealth() > 0)
        {
            s1.SetGameStatus(PlayerStatus.GameStatus.Loser);
            s2.SetGameStatus(PlayerStatus.GameStatus.Winner);
        }
    }

    public GameObject getPlayerOne()
    {
        return playerOne;
    }

    public GameObject getPlayerTwo()
    {
        return playerTwo;
    }
}

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

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.Find("Sound Player");
        seconds = 0;
        maxTime = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if(players == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            playerOne = players[1];
            playerTwo = players[2];
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
        // in case of a tie
        if(p1.getHealth() <= 0 && p2.getHealth() <= 0)
        {
            s1.SetGameStatus(PlayerStatus.GameStatus.Tie);
            s2.SetGameStatus(PlayerStatus.GameStatus.Tie);
        }
        // player 1 wins
        else if(p1.getHealth() > 0 && p2.getHealth() <= 0)
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
        timer1.GetComponent<TextMesh>().text = "Time Left: " + ((int)(maxTime - seconds)).ToString();
        timer2.GetComponent<TextMesh>().text = "Time Left: " + ((int)(maxTime - seconds)).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum GameStatus
    {
        None, Winner, Loser, Tie
    }
    private GameStatus status;
    // Start is called before the first frame update
    void Start()
    {
        status = GameStatus.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(status == GameStatus.Tie)
        {
            Debug.Log("Tie");
        }
        else if(status == GameStatus.Winner)
        {
            Debug.Log("Winner");
        }
        else if(status == GameStatus.Loser)
        {
            Debug.Log("Loser");
        }
    }

    public void SetGameStatus(GameStatus status)
    {
        this.status = status;
    }
}

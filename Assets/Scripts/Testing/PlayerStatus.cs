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
    private bool playWinOrLose;
    private GameObject soundPlayer;
    public GameObject WinnerText, LoserText, TieText;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.Find("Sound Player");
        status = GameStatus.None;
        playWinOrLose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(status == GameStatus.Tie)
        {
            Debug.Log("Tie");
            if (!playWinOrLose)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/Loser", soundPlayer.gameObject.transform.position);
                Instantiate(TieText, this.gameObject.transform);
                playWinOrLose = true;
            }
        }
        else if(status == GameStatus.Winner)
        {
            Debug.Log("Winner");
            if (!playWinOrLose)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/Winner", soundPlayer.gameObject.transform.position);
                Instantiate(WinnerText, this.gameObject.transform);
                playWinOrLose = true;
            }
        }
        else if(status == GameStatus.Loser)
        {
            Debug.Log("Loser");
            if (!playWinOrLose)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/Loser", soundPlayer.gameObject.transform.position);
                Instantiate(LoserText, this.gameObject.transform);
                var fighterScripts = gameObject.GetComponent<FighterInput>();
                fighterScripts.enabled = false;
                var moveScript = gameObject.GetComponent<FighterVRMovement>();
                moveScript.enabled = false;
                playWinOrLose = true;
            }
        }
    }

    public void SetGameStatus(GameStatus status)
    {
        this.status = status;
    }
}

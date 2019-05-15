using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public enum GameStatus
    {
        None, Winner, Loser, Tie
    }
    private GameStatus status;
    private bool playWinOrLose, playIntro;
    private GameObject soundPlayer;
    public GameObject WinnerText, LoserText, TieText, fireworks;
    private TrackPlayers trackPlayers;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.Find("Sound Player");
        trackPlayers = GameObject.Find("PlayersTracker").GetComponent<TrackPlayers>();

        status = GameStatus.None;
        playWinOrLose = false;
        playIntro = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if time is greater than 2 seconds to remove an error
        if (trackPlayers != null && !playIntro && Time.timeSinceLevelLoad >= 2.0)
        {
            if (trackPlayers.getPlayerOne().Equals(this.gameObject))
            {
                StartCoroutine(PlayerOneIntroSpeech());
            }
            else if (trackPlayers.getPlayerTwo().Equals(this.gameObject))
            {
                StartCoroutine(PlayerTwoIntroSpeech());
            }
            playIntro = true;
        }

        if (status == GameStatus.Tie)
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
                Instantiate(fireworks, this.gameObject.transform);
                playWinOrLose = true;
                StartCoroutine(VictorySpeech());
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

    IEnumerator VictorySpeech()
    {
        yield return new WaitForSeconds(2.5f);
        if (this.gameObject.GetComponent<MechaVO>() != null)
        {
            this.gameObject.GetComponent<MechaVO>().Victory();
        }
        else
        {
            this.gameObject.GetComponent<UCsVO>().Victory();
        }
    }

    IEnumerator PlayerOneIntroSpeech()
    {
        yield return new WaitForSeconds(0.5f);
        if (this.gameObject.GetComponent<MechaVO>() != null)
        {
            this.gameObject.GetComponent<MechaVO>().Intros();
        }
        else
        {
            this.gameObject.GetComponent<UCsVO>().Intros();
        }
    }

    IEnumerator PlayerTwoIntroSpeech()
    {
        yield return new WaitForSeconds(2.5f);
        if (this.gameObject.GetComponent<MechaVO>() != null)
        {
            this.gameObject.GetComponent<MechaVO>().Intros();
        }
        else
        {
            this.gameObject.GetComponent<UCsVO>().Intros();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera miniMapCamera;
    //[SerializeField] GameObject playerPrefab;
    private GameObject playerPrefab;
    public GameObject defaultPlayer;
    [SerializeField] GameObject healthbar;
    [SerializeField] GameObject healthbartop;
    [SerializeField] ParticleSystem sparkles;
    private PrefabSelection playerSelection;
    private GameObject g;
    private Fighter fighter;
    [SerializeField] GameObject Sphere;
        
    void Start() {
        g = GameObject.Find("Player Selector");
        playerSelection = g.gameObject.GetComponent<PrefabSelection>();
        // gives me bug
        playerPrefab = playerSelection.getRoster()[PlayerPrefs.GetString("CharacterSelection", "Mecha")];
        Debug.Log(playerPrefab);
        if(playerPrefab == null)
        {
            playerPrefab = defaultPlayer;
        }
        SpawnPlayer();
        playSound(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpawnPlayer() {
        GameObject _player = Instantiate(playerPrefab, new Vector3( 0 , 0 , 0), new Quaternion(0,0,0,0));
        Fighter fighter = _player.GetComponent<Fighter>();
        _player.transform.position = new Vector3(0, 0, -10);
        _player.GetComponent<FPLook>().AttachCamera(mainCamera);
        healthbar.GetComponent<Bar>().setCamera(mainCamera);
        healthbar.GetComponent<Bar>().setFighter(fighter);
        fighter.ResetFighterHealth();
        miniMapCamera.GetComponent<Minimap>().setFighter(fighter);
        Sphere.GetComponent<Sphere>().setFighter(fighter);
        healthbartop.GetComponent<healthbar>().setFighter(fighter);
    }
    public void SelectPlayer(GameObject prefab, Transform transform)
    {

        ParticleSystem fireworks = Instantiate(sparkles, transform.position,new Quaternion(0,0,0,0));
        
        GameObject _player = Instantiate(prefab, transform.position, transform.rotation);
        FPLook fplook = _player.GetComponent<FPLook>();
        Minimap minimap = _player.GetComponent<Minimap>();
        Debug.Log(fplook);
        _player.GetComponent<FPLook>().AttachCamera(mainCamera);
    }

    // getPlayer() is called on Box.cs
    public GameObject getPlayer()
    {
        return playerPrefab;
    }

    public Camera getCamera()
    {
        return mainCamera;
    }

    private void playSound(int sceneIndex)
    {
        if(sceneIndex == 2)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/RoundOne", this.gameObject.transform.position);
        }
        else if (sceneIndex == 3)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/RoundTwo", this.gameObject.transform.position);
        }
        else if (sceneIndex == 4)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Announcer/RoundFinal", this.gameObject.transform.position);
        }
    }
}

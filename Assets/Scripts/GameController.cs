using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    //[SerializeField] GameObject playerPrefab;
    private GameObject playerPrefab;
    [SerializeField] GameObject healthbar;
    [SerializeField] ParticleSystem sparkles;
    private PrefabSelection playerSelection;
    private GameObject g;
    private Fighter fighter;
        
    void Start() {
        g = GameObject.Find("Player Selector");
        playerSelection = g.gameObject.GetComponent<PrefabSelection>();
        PlayerPrefs.DeleteKey("CharacterSelection");
        playerPrefab = playerSelection.getRoster()[PlayerPrefs.GetString("CharacterSelection", "Mecha")];
        SpawnPlayer();
    }

    public void SpawnPlayer() {
        GameObject _player = Instantiate(playerPrefab, new Vector3( 0 , 0 , 0), new Quaternion(0,0,0,0));
        Fighter fighter = _player.GetComponent<Fighter>();
        _player.transform.position = new Vector3(0, 0, -10);
        _player.GetComponent<FPLook>().AttachCamera(mainCamera);
        healthbar.GetComponent<Bar>().setCamera(mainCamera);
        healthbar.GetComponent<Bar>().setFighter(fighter);
        fighter.resetFighter();
    }
    public void SelectPlayer(GameObject prefab, Transform transform)
    {

        ParticleSystem fireworks = Instantiate(sparkles, transform.position,new Quaternion(0,0,0,0));
        
        GameObject _player = Instantiate(prefab, transform.position, transform.rotation);
        FPLook fplook = _player.GetComponent<FPLook>();
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
}

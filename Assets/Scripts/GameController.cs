using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject playerPrefab;

    void Start() {
        SpawnPlayer();
    }

    public void SpawnPlayer() {
        GameObject _player = Instantiate(playerPrefab);
        _player.transform.position = new Vector3(0, 0, -10);
        _player.GetComponent<FPLook>().AttachCamera(mainCamera);
    }

    public GameObject getPlayer()
    {
        return playerPrefab;
    }

    public Camera getCamera()
    {
        return mainCamera;
    }
}

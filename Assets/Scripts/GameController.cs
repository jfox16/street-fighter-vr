﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] ParticleSystem sparkles;

    void Start() {
        SpawnPlayer();
    }

    public void SpawnPlayer() {
        GameObject _player = Instantiate(playerPrefab, new Vector3( 0 , 0 , 0), new Quaternion(0,0,0,0));
        _player.transform.position = new Vector3(0, 0, -10);
        _player.GetComponent<FPLook>().AttachCamera(mainCamera);
    }
    public void SelectPlayer(GameObject prefab, Transform transform)
    {
        Debug.Log(transform.position);

        ParticleSystem fireworks = Instantiate(sparkles, transform.position,new Quaternion(0,0,0,0));
        
        GameObject _player = Instantiate(prefab, transform.position, transform.rotation);
        FPLook fplook = _player.GetComponent<FPLook>();
        Debug.Log(fplook);
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

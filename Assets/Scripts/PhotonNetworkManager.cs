﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNetworkManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject playerPrefab;
   

    void Start() {
        SpawnPlayer();
    }

    public void SpawnPlayer() {

        GameObject _player = Instantiate(playerPrefab);
        _player.transform.position = new Vector3(0, 0, -10);
        _player.GetComponent<FighterLook>().AttachCamera(mainCamera);
    }
}

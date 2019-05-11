using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * Spawns a Fighter for player. Spawns on the network if you are 
 * currently connected to a room. Chooses prefab based on 
 * GameControllerDDOL.selectedFighter.
 */
public class FighterSpawner : MonoBehaviour
{
    string mechaPath = "Characters/VR_Mecha";
    string unitychanPath = "Characters/VR_Unitychan";

    void Start() {
        SpawnFighter();
    }

    void SpawnFighter() 
    {
        if (GameControllerDDOL.spawnedFighter != null) 
            return;

        if (PhotonNetwork.CurrentRoom == null) 
        {
            switch (GameControllerDDOL.selectedFighter) {
                case GameControllerDDOL.Fighter.Mecha:
                    GameObject mechaPrefab = (GameObject)Resources.Load(mechaPath);
                    GameControllerDDOL.spawnedFighter = Instantiate(mechaPrefab, transform.position, transform.rotation);
                    break;
                case GameControllerDDOL.Fighter.Unitychan:
                    GameObject unitychanPrefab = (GameObject)Resources.Load(unitychanPath);
                    GameControllerDDOL.spawnedFighter = Instantiate(unitychanPrefab, transform.position, transform.rotation);
                    break;
                default:
                    Debug.Log("No Fighter selected for FighterSpawner!");
                    break;
            }
        }
        else 
        {
            switch (GameControllerDDOL.selectedFighter) {
                case GameControllerDDOL.Fighter.Mecha:
                    GameControllerDDOL.spawnedFighter = PhotonNetwork.Instantiate(mechaPath, transform.position, transform.rotation);
                    break;
                case GameControllerDDOL.Fighter.Unitychan:
                    GameControllerDDOL.spawnedFighter = PhotonNetwork.Instantiate(unitychanPath, transform.position, transform.rotation);
                    break;
                default:
                    Debug.Log("No Fighter selected for FighterSpawner!");
                    break;
            }
        }
    }
}

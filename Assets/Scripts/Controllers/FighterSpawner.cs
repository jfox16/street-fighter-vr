using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSpawner : MonoBehaviour
{
    [SerializeField] GameObject mechaPrefab;
    [SerializeField] GameObject unitychanPrefab;

    void Start() {
        SpawnFighter();
    }

    void SpawnFighter() {
        if (GameControllerDDOL.Instance == null || GameControllerDDOL.spawnedFighter != null) return;

        GameObject spawnedFighter = null;

        switch (GameControllerDDOL.selectedFighter) {
            case GameControllerDDOL.Fighter.Mecha:
                spawnedFighter = Instantiate(mechaPrefab, transform.position, transform.rotation);
                break;
            case GameControllerDDOL.Fighter.Unitychan:
                spawnedFighter = Instantiate(unitychanPrefab, transform.position, transform.rotation);
                break;
            default:
                Debug.Log("No Fighter selected for FighterSpawner!");
                break;
        } 

        GameControllerDDOL.spawnedFighter = spawnedFighter;
    }
}

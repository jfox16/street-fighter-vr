using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switches to another scene when Hurt.
public class FighterSelectWarp : Unit
{
    [SerializeField] GameControllerDDOL.Fighter selectFighter;

    string mechaPath     = "Characters/VR_Mecha";
    string unitychanPath = "Characters/VR_Unitychan";

    public override void Hurt(float damage) 
    {
        if (GameControllerDDOL.spawnedFighter == null) return;
        GameControllerDDOL.selectedFighter = selectFighter;
        GameObject _currentFighter = GameControllerDDOL.spawnedFighter;
        _currentFighter.SetActive(false);

        switch (GameControllerDDOL.selectedFighter) 
        {
            case GameControllerDDOL.Fighter.Mecha:
                GameObject mechaPrefab = (GameObject)Resources.Load(mechaPath);
                GameControllerDDOL.spawnedFighter = Instantiate(
                    mechaPrefab, 
                    _currentFighter.transform.position, 
                    _currentFighter.transform.rotation
                );
                break;
            case GameControllerDDOL.Fighter.Unitychan:
                GameObject unitychanPrefab = (GameObject)Resources.Load(unitychanPath);
                GameControllerDDOL.spawnedFighter = Instantiate(
                    unitychanPrefab, 
                    _currentFighter.transform.position, 
                    _currentFighter.transform.rotation
                );
                break;
            default:
                Debug.Log("No Fighter selected for FighterSelectWarp!");
                break;
        }

        Destroy(_currentFighter);
    }
}

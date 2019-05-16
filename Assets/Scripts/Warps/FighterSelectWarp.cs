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
        if (GameControllerDDOL.spawnedFighter == null || GameControllerDDOL.selectedFighter == selectFighter)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI-SFX/UI-cancel", this.gameObject.transform.position);
            return;
        }
        GameControllerDDOL.selectedFighter = selectFighter;
        GameObject _oldFighter = GameControllerDDOL.spawnedFighter;
        _oldFighter.SetActive(false);
        switch (GameControllerDDOL.selectedFighter) 
        {
            case GameControllerDDOL.Fighter.Mecha:
                GameObject mechaPrefab = (GameObject)Resources.Load(mechaPath);
                GameControllerDDOL.spawnedFighter = Instantiate(
                    mechaPrefab, 
                    _oldFighter.transform.position, 
                    _oldFighter.transform.rotation
                );
                break;
            case GameControllerDDOL.Fighter.Unitychan:
                GameObject unitychanPrefab = (GameObject)Resources.Load(unitychanPath);
                GameControllerDDOL.spawnedFighter = Instantiate(
                    unitychanPrefab, 
                    _oldFighter.transform.position, 
                    _oldFighter.transform.rotation
                );
                break;
            default:
                Debug.Log("No Fighter selected for FighterSelectWarp!");
                break;
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI-SFX/UI-select", this.gameObject.transform.position);
        GameControllerDDOL.spawnedFighter.GetComponent<FighterAnimationHandler>().SetTeam(Unit.Team.Red);
        GameControllerDDOL.spawnedFighter.GetComponent<CharacterVO>().Intros();
        
        Destroy(_oldFighter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerDDOL : MonoBehaviour
{
    public static GameControllerDDOL Instance = null;

    public enum Fighter{Default, Mecha, Unitychan}
    public static GameControllerDDOL.Fighter selectedFighter = GameControllerDDOL.Fighter.Mecha;
    public static GameObject spawnedFighter = null;

    void Awake() {
        if (Instance == null) {
            // Set this as Instance and don't destroy it across scenes.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            // If an instance of this already exists, destroy this one.
            Destroy(gameObject);
        }
    }
}

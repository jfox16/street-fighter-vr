using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * GameControllerDDOL is a new implementation of the GameController. Other GameObjects
 * can access important data through its static methods and variables.
 * GameControllerDDOL.selectedFighter is used to keep track of which character the player has selected.
 * GameControllerDDOL.spawnedFighter is used to keep track of which Fighter the player is currently controlling.
 */
public class GameControllerDDOL : MonoBehaviour
{
    public static GameControllerDDOL Instance = null;

    public enum Fighter{Default, Mecha, Unitychan}
    public static GameControllerDDOL.Fighter selectedFighter = GameControllerDDOL.Fighter.Mecha;
    public static GameObject spawnedFighter = null;

    public static int collisionMask;

    void Awake() {
        if (Instance == null) {
            // Set this as Instance and keep it from being destroyed across scenes.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            // If an instance of this already exists, destroy this one.
            Destroy(gameObject);
            return;
        }

        collisionMask = LayerMask.NameToLayer("Collision");
    }

    // Returns true if the current scene is a versus scene.
    public bool CheckVersusScene() {
        int _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        return (_sceneIndex > 2 && _sceneIndex < 6);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switches to another scene when Hurt.
public class Warp : Unit
{
    [SerializeField] string destinationScene;

    public override void Hurt(float damage) {
        SceneManager.LoadScene(destinationScene);
    }
}

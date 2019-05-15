using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switches to another scene when Hurt.
public class Warp : Unit
{
    [SerializeField] string destinationScene;

    public override void Hurt(float damage) {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI-SFX/UI-select", this.gameObject.transform.position);
        SceneManager.LoadScene(destinationScene);
    }
}

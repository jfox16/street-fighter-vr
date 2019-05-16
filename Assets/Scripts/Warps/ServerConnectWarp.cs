using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Connects to Server when Hurt.
public class ServerConnectWarp : Unit
{
    public override void Hurt(float damage) {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI-SFX/UI-select", this.gameObject.transform.position);
        NetworkController.ConnectToServer();
    }
}

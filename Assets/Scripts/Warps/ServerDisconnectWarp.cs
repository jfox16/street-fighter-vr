using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Disconnects from Server when Hurt.
public class ServerDisconnectWarp : Unit
{
    public override void Hurt(float damage) {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI-SFX/UI-cancel", this.gameObject.transform.position);
        NetworkController.DisconnectFromServer();
    }
}

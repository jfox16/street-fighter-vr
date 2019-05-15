using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Disconnects from Server when Hurt.
public class ServerDisconnectWarp : Unit
{
    public override void Hurt(float damage) {
        NetworkController.DisconnectFromServer();
    }
}

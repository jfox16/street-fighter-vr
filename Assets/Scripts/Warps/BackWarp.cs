using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

// Switches to another scene when Hurt.
public class BackWarp : Unit
{
    string startScene = "VR Start";
    string lobbyScene = "Scenes/Multiplayer/MultiplayerLobby";

    public override void Hurt(float damage) {
        if (PhotonNetwork.CurrentRoom == null) {
            SceneManager.LoadScene(startScene);
        }
        else {
            NetworkController.LeaveRoom();
        }
    }
}

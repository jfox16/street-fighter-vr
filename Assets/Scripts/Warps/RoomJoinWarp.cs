using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switches to another scene when Hurt.
public class RoomJoinWarp : Unit
{
    [SerializeField] string photonRoomName = "";
    [SerializeField] int sceneIndex = 0;

    public override void Hurt(float damage) {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI-SFX/UI-select", this.gameObject.transform.position);
        NetworkController.JoinRoom(photonRoomName, sceneIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switches to another scene when Hurt.
public class RoomJoinWarp : Unit
{
    [SerializeField] string roomName = "";
    [SerializeField] int sceneIndex = 0;

    public override void Hurt(float damage) {
        NetworkController.JoinRoom(roomName, sceneIndex);
    }
}

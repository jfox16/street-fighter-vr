using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkFighterAct : FighterAct
{
    protected PhotonView photonView;

    protected new void Awake() {
        photonView = GetComponent<PhotonView>();
        base.Awake();
    }

    protected new void Update() {
        // Only read input if this character belongs to client
        if (photonView.IsMine) {
            ReadInput();
        }
    }
}

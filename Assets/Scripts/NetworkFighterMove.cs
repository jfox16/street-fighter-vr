using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// This class inherits from FighterMove
public class NetworkFighterMove : FighterMove
{
    protected PhotonView photonView;

    protected new void Awake() {
        photonView = GetComponent<PhotonView>();
        base.Awake();
    }

    protected new void FixedUpdate() {
        // Only read input if this character belongs to client
        if (photonView.IsMine) {
            ReadInput();
            Move();
        }
    }
    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(moveVec);
        }
        else
        {
            moveVec = (Vector3)stream.ReceiveNext();
        }
    }
    
}

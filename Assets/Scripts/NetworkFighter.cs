using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkFighter : Fighter
{
    protected PhotonView photonView;

    protected new void Awake() {
        photonView = GetComponent<PhotonView>();
        base.Awake();
    }

    public override void Hurt(float damage) {
        photonView.RPC("ClientHurt", RpcTarget.All, damage);
    }

    [PunRPC]
    public void ClientHurt(float damage) {
        Debug.Log(gameObject + " IS HURT " + damage);
        animator.SetTrigger("Hurt");
    }
}

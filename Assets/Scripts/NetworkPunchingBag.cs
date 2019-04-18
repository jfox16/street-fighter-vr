using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPunchingBag : Fighter
{
    protected PhotonView photonView;

    protected new void Awake() {
        photonView = GetComponent<PhotonView>();
        base.Awake();
    }

    public override void Hurt(float damage) {
        if (photonView.IsMine) {
            photonView.RPC("ClientHurt", RpcTarget.All, damage);
        }
    }

    [PunRPC]
    public void ClientHurt(float damage) {
        health -= damage;
        if(health <= 0) Die();
        Debug.Log(gameObject.name + "is Hurt");
        animator.SetTrigger("Hurt");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkFighter : Unit
{
    [SerializeField] float health = 100;

    [SerializeField] GameObject punchPrefab;

    Animator animator;
    FPLook fpLook;
    Transform punchPointTransform;
    PhotonView photonView;



    #region UNITY CALLBACKS

    void Awake() {
        animator = GetComponent<Animator>();
        fpLook = GetComponent<FPLook>();
        punchPointTransform = transform.Find("Punch Point");
        photonView = GetComponent<PhotonView>();
    }

    void Update() {
        /* photonView.IsMine checks if this object belongs to the player on this client.
        If it does, then read input. This will prevent your input from controlling other 
        player's Fighters. */
        if (!photonView.IsMine)
            return;
            
        if (Input.GetButtonDown("Punch") || Input.GetMouseButtonDown(0)) {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("Punch");
        }
        else if (Input.GetButtonDown("Kick") || Input.GetMouseButtonDown(1)) {
            animator.SetTrigger("Kick");
        }
    }

    #endregion


    public override void Hurt(float damage) {
        health -= damage;
        if(health <= 0) Die();
    }

    void Die() {
        Destroy(gameObject);
    }
}

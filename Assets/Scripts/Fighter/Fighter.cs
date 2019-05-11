using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fighter : Unit
{
    public float health = 1000000;
    public float special = 0;
    public float specialCost = 10;
    public int fighterid;
    public float damageModifier = 1.0f;

    Animator animator;
    FPLook fpLook;
    PhotonView photonView;

    Transform punchPointTransform;
    Transform kickPointTransform;
    Transform projectilePointTransform;
    public GameObject projectilePoint;
    public static int numberOfProjectiles;
    public int cooldown;
    private float timestamp;

    public bool isMine = false;

    void Awake() {
        animator   = GetComponent<Animator>();
        fpLook     = GetComponent<FPLook>();
        photonView = GetComponent<PhotonView>();

        punchPointTransform = transform.Find("Punch Point");
        kickPointTransform = transform.Find("Kick Point");
    }

    void Start() {
        if (PhotonNetwork.CurrentRoom == null || photonView.IsMine) {
            isMine = true;
        }
    }

    public override void Hurt(float damage) {
        if (animator.GetBool("Block"))
        {
            health -= damage * 0.5f;
        }
        else
        {
            animator.SetTrigger("Hurt");
            health -= damage;
         //   flash.PlayerHit();
        }
        Debug.Log(health);
        if (health < 0)
        {
            health = 0;
        }
        if (health <= 0) Die();
    }

    public float getHealth()
    {
        return health;
    }

    void Die() {
        animator.SetTrigger("Die");
    }
    public void ResetFighterHealth()
    {
        health = 100;
        animator.SetTrigger("Idle");
    }
    public void gainSpecial(float val)
    {
        special += val;
        if(special > 100f)
        {
            special = 100f;
        }else if ( special < 0)
        {
            special = 0;
        }
    }
}

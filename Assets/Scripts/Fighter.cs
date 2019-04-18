using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    public float health = 100;

    [SerializeField] GameObject punchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] FlashScript flash;

    public int maxProjectiles;

    Animator animator;
    FPLook fpLook;
    Transform punchPointTransform;
    Transform kickPointTransform;
    Transform projectilePointTransform;
    public GameObject projectilePoint;
    public static int numberOfProjectiles;
    public int cooldown;
    private float timestamp;
    void Awake() {
        animator = GetComponent<Animator>();
        fpLook = GetComponent<FPLook>();
        punchPointTransform = transform.Find("Punch Point");
        kickPointTransform = transform.Find("Kick Point");
        flash = GameObject.Find("Flash").GetComponent<FlashScript>();
        
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
            flash.PlayerHit();
        }
        Debug.Log(health);
        if (health < 0)
        {
            health = 0;
        }
        if (health <= 0) Die();
    }
    void Die() {
        animator.SetTrigger("Die");
    }
    public float getHealth()
    {
        return health;
    }
    public void resetFighter()
    {
        health = 100;
        animator.SetTrigger("Idle");
    }
}

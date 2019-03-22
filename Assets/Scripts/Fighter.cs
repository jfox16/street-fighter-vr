﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    [SerializeField] float health = 100;

    [SerializeField] GameObject punchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject projectilePrefab;

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
        
    }

    void Update() {

    }
    void Punch()
    {
        Debug.Log("punched");
        Instantiate(punchPrefab, punchPointTransform);
        animator.SetTrigger("Punch");
        animator.SetTrigger("Jab");
    }
    public override void Hurt(float damage) {
        if (!animator.GetBool("Block"))
        {
            health -= damage;
        }
        Debug.Log(health);
        if (health <= 0) Die();
    }
    void Die() {
        Destroy(gameObject);
    }
   
    public void removeProjectile()
    {
        numberOfProjectiles--;
        Debug.Log(numberOfProjectiles);
    }
}

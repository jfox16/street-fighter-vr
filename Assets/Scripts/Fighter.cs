using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{

    public float health = 100;
    protected Animator animator;

    protected void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void Hurt(float damage) {
        health -= damage;
        if(health <= 0) Die();
        ScreenFlash.PlayerHit();
    }

    protected void Die() {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{

    [SerializeField] protected float health = 100;

    protected void Awake() {
        // Nothing yet
    }

    public override void Hurt(float damage) {
        health -= damage;
        if(health <= 0) Die();
    }

    protected void Die() {
        Destroy(gameObject);
    }
}

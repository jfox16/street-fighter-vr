using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] int health = 100;

    public void Hurt(int damage) {
        health -= damage;
        if(health <= 0) Die();
    }

    void Die() {
        Destroy(gameObject);
    }
}

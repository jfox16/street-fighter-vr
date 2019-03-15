using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGuy : Unit
{
    Animator animator;
    private float _health;
    private float epsilon = 0.000001f;

    void Awake() {
        animator = GetComponent<Animator>();
        _health = 5.0f;
        animator.SetBool("Dead", false);
    }

    public override void Hurt(float damage) {
        _health -= damage;
        // check if the health is 0
        if(_health <= epsilon)
        {
            animator.SetBool("Dead", true);
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGuy : Unit
{
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void Hurt(float damage) {
        animator.SetTrigger("Hurt");
    }
}

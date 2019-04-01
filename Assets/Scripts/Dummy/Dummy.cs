using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void HitHead() {
        animator.SetTrigger("Hit Head");
    }

    public void HitBody() {
        animator.SetTrigger("Hit Body");
    }
}

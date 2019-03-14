using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    [SerializeField] float health = 100;

    [SerializeField] GameObject punchPrefab;

    Animator animator;
    FPLook fpLook;
    Transform punchPointTransform;



    #region UNITY CALLBACKS

    void Awake() {
        animator = GetComponent<Animator>();
        fpLook = GetComponent<FPLook>();
        punchPointTransform = transform.Find("Punch Point");
    }

    void Update() {
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

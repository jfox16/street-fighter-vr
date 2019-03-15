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



    void Awake() {
        animator = GetComponent<Animator>();
        fpLook = GetComponent<FPLook>();
        punchPointTransform = transform.Find("Punch Point");
    }

    void Update() {
        if (Input.GetButtonDown("Punch") || Input.GetMouseButtonDown(0)) {
            Invoke("Punch", .1f);
        }
        else if (Input.GetButtonDown("Kick") || Input.GetMouseButtonDown(1)) {
            animator.SetTrigger("Rising_P");
        }
    }
    void Punch()
    {
        Debug.Log("punched");
        Instantiate(punchPrefab, punchPointTransform);
        animator.SetTrigger("Punch");
        animator.SetTrigger("Jab");
    }
    public override void Hurt(float damage) {
        health -= damage;
        Interrupt();
        if (health <= 0) Die();
    }
    void Interrupt()
    {   
        CancelInvoke();
    }
    void Die() {
        Destroy(gameObject);
    }
}

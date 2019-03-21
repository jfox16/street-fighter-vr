using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    [SerializeField] float health = 100;

    [SerializeField] GameObject punchPrefab;
    [SerializeField] GameObject kickPrefab;

    CharacterController controller;
    Animator animator;
    FPLook fpLook;
    Transform punchPointTransform;
    Transform kickPointTransform;



    void Awake() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        fpLook = GetComponent<FPLook>();
        punchPointTransform = transform.Find("Punch Point");
        kickPointTransform = transform.Find("Kick Point");
    }

    void Update() {

        if (Input.GetKeyUp("1"))
        {
            animator.SetBool("Block", false);
        }

        if (Input.GetButtonDown("Punch") || Input.GetMouseButtonDown(0)) {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("Punch");
        }
        else if(Input.GetButtonDown("Punch Left"))
        {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("PunchLeft");
        }
        else if (Input.GetButtonDown("Kick") || Input.GetMouseButtonDown(1)) {
            Instantiate(kickPrefab, kickPointTransform);
            animator.SetTrigger("Kick");
        }
        else if (Input.GetButtonDown("Kick Left"))
        {
            Instantiate(kickPrefab, kickPointTransform);
            animator.SetTrigger("KickLeft");
        }
        else if (Input.GetKeyDown("1"))
        {
            animator.SetBool("Block", true);
        }
    }

    public override void Hurt(float damage) {
        if (!animator.GetBool("Block"))
        {
            health = health - damage;
            Debug.Log(damage);
        }
        else
        {
            Debug.Log("Blocking");
            Vector3 _moveBack = transform.forward * -0.75f;
            controller.Move(_moveBack);
        }
        if (health <= 0) Die();
    }

    void Die() {
        Destroy(gameObject);
    }
}

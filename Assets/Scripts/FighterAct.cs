using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAct : MonoBehaviour
{
    protected Animator animator;



    #region UNITY CALLBACKS

    protected void Awake() {
        animator = GetComponent<Animator>();
    }

    protected void Update() {
        ReadInput();
    }

    #endregion

    

    #region OTHER METHODS

    // Reads player input and performs action
    protected void ReadInput() {
        if (Input.GetButtonDown("Punch") || Input.GetMouseButtonDown(0)) {
            PerformPunch();
        }
        else if (Input.GetButtonDown("Kick") || Input.GetMouseButtonDown(1)) {
            PerformKick();
        }
    }

    protected void PerformPunch() {
        Debug.Log(gameObject.name + " punches!");
        animator.SetTrigger("Punch");
    }
    protected void PerformKick() {
        Debug.Log(gameObject.name + " kicks!");
        animator.SetTrigger("Kick");
    }

    #endregion
}

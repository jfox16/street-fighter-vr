using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAct : MonoBehaviour
{

    protected Animator animator;
    protected Fighter fighter;

    [SerializeField] Attack rightHandAttack;
    [SerializeField] Attack rightFootAttack;



    #region UNITY CALLBACKS

    protected void Awake() {
        animator = GetComponent<Animator>();
        fighter = GetComponent<Fighter>();
        rightHandAttack.ownerTeam = fighter.team;
    }

    protected void Update() {
        ReadInput();
    }

    #endregion

    

    #region PRIVATE METHODS

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
        animator.SetTrigger("Punch");
    }
    protected void PerformKick() {
        animator.SetTrigger("Kick");
    }

    #endregion



    #region PUBLIC METHODS

    public void EnableRightHand() {
        rightHandAttack.SetEnabled(true);
    }

    public void DisableRightHand() {
        rightHandAttack.SetEnabled(false);
    }

    public void EnableRightFoot() {
        rightFootAttack.SetEnabled(true);
    }

    public void DisableRightFoot() {
        rightFootAttack.SetEnabled(false);
    }

    #endregion
}

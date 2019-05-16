using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * FighterInput 
 */
public abstract class FighterInput : MonoBehaviour
{
    public Fighter fighter;
    public Animator animator;
    PhotonView photonView;

    protected void Awake() {
        fighter = GetComponent<Fighter>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }

    protected void Update()
    {
        // Only read input if this Fighter belongs to the client.
        if (fighter.isMine) {
            ReadAttackInput();
        }
    }

    void ReadAttackInput() 
    {
        if (!animator.GetBool("isAttacking") && !animator.GetBool("isWalking") && fighter.isAlive)
        {
            if (VRInputHandler.GetInput("Special") || Input.GetButtonDown("Special")) {
                animator.SetTrigger("Special");
            }
            else if (VRInputHandler.GetInput("Kick") || Input.GetButtonDown("Kick")) {
                animator.SetTrigger("Kick");
            }
            else if (VRInputHandler.GetInput("Right Smash") || Input.GetButtonDown("Right Smash")) {
                animator.SetTrigger("Right Smash");
            }
            else if (VRInputHandler.GetInput("Left Punch") || Input.GetButtonDown("Left Punch") || Input.GetMouseButton(0)) {
                animator.SetTrigger("Left Punch");
            }
            else if (VRInputHandler.GetInput("Right Punch") || Input.GetButtonDown("Right Punch") || Input.GetMouseButton(1)) {
                animator.SetTrigger("Right Punch");
            }
        }
    }

    public abstract void punch();
    public abstract void kick();
    public abstract void special();
}

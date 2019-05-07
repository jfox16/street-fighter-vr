using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * FighterAnimationHandler is how the Animator interacts with the rest of the Fighter.
 * These functions are called directly from animations.
 * EnableLimbCollider and DisableLimbCollider are used in attack animations.
 * Specific implementations for Fighters should be implemented in a child class.
 * (Use MechaAnimationHandler as an example)
 */
public class FighterAnimationHandler : MonoBehaviour
{    
    public enum Limb {LeftArm, RightArm, LeftLeg, RightLeg}

    [SerializeField] Attack leftArmAttack;
    [SerializeField] Attack rightArmAttack;
    [SerializeField] Attack leftLegAttack;
    [SerializeField] Attack rightLegAttack;
    Animator animator;
    PhotonView photonView;

    void Awake()
    {
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        rightArmAttack.collider.enabled = false;
        leftArmAttack.collider.enabled = false;
        rightLegAttack.collider.enabled = false;
        leftLegAttack.collider.enabled = false;
    }

    public void SetAttackingTrue() 
    {
        animator.SetBool("isAttacking", true);
    }

    public void SetAttackingFalse() 
    {
        animator.SetBool("isAttacking", false);
    }

    public void EnableLimbCollider(Limb limb, float damage) 
    {
        // Only do this if this Fighter belongs to the client.
        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }

        switch(limb) {
            case Limb.LeftArm:
                leftArmAttack.collider.enabled = true;
                leftArmAttack.damage = damage;
                break;
            case Limb.RightArm:
                rightArmAttack.collider.enabled = true;
                rightArmAttack.damage = damage;
                break;
            case Limb.LeftLeg:
                leftLegAttack.collider.enabled = true;
                leftLegAttack.damage = damage;
                break;
            case Limb.RightLeg:
                rightLegAttack.collider.enabled = true;
                rightLegAttack.damage = damage;
                break;
        }
    }

    public void DisableLimbCollider(Limb limb) 
    {
        // Only do this if this Fighter belongs to the client.
        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }
        
        switch(limb) {
            case Limb.LeftArm:
                leftArmAttack.collider.enabled = false;
                leftArmAttack.damage = 0;
                break;
            case Limb.RightArm:
                rightArmAttack.collider.enabled = false;
                rightArmAttack.damage = 0;
                break;
            case Limb.LeftLeg:
                leftLegAttack.collider.enabled = false;
                leftLegAttack.damage = 0;
                break;
            case Limb.RightLeg:
                rightLegAttack.collider.enabled = false;
                rightLegAttack.damage = 0;
                break;
        }
    }
}

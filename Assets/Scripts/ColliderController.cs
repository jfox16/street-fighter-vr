using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Collider rightArmCollider;
    [SerializeField] Collider leftArmCollider;
    [SerializeField] Collider rightLegCollider;
    [SerializeField] Collider leftLegCollider;
    [SerializeField] Collider hurtBox;
    Animator _animator;

    void Start()
    {
        rightArmCollider.enabled = false;
        leftArmCollider.enabled = false;
        rightLegCollider.enabled = false;
        leftLegCollider.enabled = false;
        _animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Arm()
    {
        rightArmCollider.enabled = true;
    }
    private void DisableRightArm()
    {
        rightArmCollider.enabled = false;
    }
    public void EnableLeftArm()
    {
        leftArmCollider.enabled = true;
    }
    private void DisableLeftArm()
    {
        leftArmCollider.enabled = false;
    }
    public void EnableLeftLeg()
    {
        leftLegCollider.enabled = true;
    }
    private void DisableLeftLeg()
    {
        leftLegCollider.enabled = false;
    }
    public void Leg()
    {
       rightLegCollider.enabled = true;
    }
    private void DisableRightLeg()
    {
        rightLegCollider.enabled = false;
    }
    //Will be decoupled later
    private void EnableMechaLightPunch()
    {
        leftArmCollider.enabled = true;
    }
    private void DisableMechaLightPunch()
    {
        leftArmCollider.enabled = false;
    }
    private void EnableMechaHeavyPunch()
    {
        rightArmCollider.enabled = true;
    }
    private void DisableMechaHeavyPunch()
    {
        rightArmCollider.enabled = false;
    }
    private void EnableMechaKick()
    {
        rightLegCollider.enabled = true;
    }
    private void DisableMechaKick()
    {
        rightLegCollider.enabled = false;
    }
    private void IsAttacking()
    {
        Debug.Log("Attacking");
        _animator.SetBool("isAttacking", true);
    }
    private void NotAttacking()
    {
        //Debug.Log("Notattacking");
        _animator.SetBool("isAttacking", false);
    }
}

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

    public int leftArmDamage;
    public int RightArmDamage;
    public int leftLegDamage;
    public int RightLegDamage;

    private Attack leftArmAttack;
    private Attack leftLegAttack;
    private Attack RightArmAttack;
    private Attack RightLegAttack;
    Animator _animator;

    void Awake()
    {
        rightArmCollider.enabled = false;
        leftArmCollider.enabled = false;
        rightLegCollider.enabled = false;
        leftLegCollider.enabled = false;
        _animator = gameObject.GetComponent<Animator>();

        leftArmAttack = leftArmCollider.gameObject.GetComponent<Attack>();
        leftLegAttack = leftLegCollider.gameObject.GetComponent<Attack>();
        RightArmAttack = rightArmCollider.gameObject.GetComponent<Attack>();
        RightLegAttack = rightLegCollider.gameObject.GetComponent<Attack>();

        leftArmAttack.damage = leftArmDamage;
        leftLegAttack.damage = leftLegDamage;
        RightArmAttack.damage = RightArmDamage;
        RightLegAttack.damage = RightLegDamage;

    }
    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableRightArmAttack(int damage)
    {
        rightArmCollider.enabled = true;
        RightArmAttack.damage = damage;
    }
    private void DisableRightArm()
    {
        rightArmCollider.enabled = false;
    }
    public void EnableLeftArmAttack(int damage)
    {
        leftArmCollider.enabled = true;
        leftArmAttack.damage = damage;
    }
    private void DisableLeftArm()
    {
        leftArmCollider.enabled = false;
    }
    public void EnableLeftLegAttack(int damage)
    {
        leftLegCollider.enabled = true;
        leftLegAttack.damage = damage;
    }
    private void DisableLeftLeg()
    {
        leftLegCollider.enabled = false;
    }
    public void EnableRightLegAttack(int damage)
    {
       rightLegCollider.enabled = true;
        RightLegAttack.damage = damage;
    }
    private void DisableRightLeg()
    {
        rightLegCollider.enabled = false;
    }
    private void IsAttacking()
    {
        _animator.SetBool("isAttacking", true);
    }
    private void NotAttacking()
    {
        _animator.SetBool("isAttacking", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaFighterInput : FighterInput
{
    public Attack LeftArmCollider;
    public Attack RightArmCollider;
    public Attack LeftLegCollider;
    public Attack RightLegCollider;

    public GameObject fireworks;
    public GameObject LeftArmParticles;
    public GameObject RightArmParticles;
    public GameObject AuraParticles;


    private void Awake()
    {
        DeactivateParticles();
    }
    public override void kick()
    {
        _animator.SetTrigger("Kick");
    }

    public override void punch()
    {
        _animator.SetTrigger("Light_Punch");
    }

    public override void special()
    {
        if(_fighter.special >= 100)
        {
            _fighter.special = 0;
            changeDamageMod(2.0f);
            _animator.SetTrigger("Special");
            Invoke("StopSpecial", 20f);
        }
    }

    private void changeDamageMod(float modifier)
    {
        LeftArmCollider.damage *= modifier;
        RightArmCollider.damage *= modifier;
        LeftLegCollider.damage *= modifier;
        RightLegCollider.damage *= modifier;
        Debug.Log("Damage modified by: " + modifier);
    }
    private void StopSpecial()
    {
        DeactivateParticles();
        changeDamageMod(1.0f);
    }
    private void DeactivateParticles()
    {
        LeftArmParticles.SetActive(false);
        RightArmParticles.SetActive(false);
        AuraParticles.SetActive(false);
    }
    private void EnableParticles()
    {
        Instantiate(fireworks, this.gameObject.transform.position, this.gameObject.transform.rotation);
        LeftArmParticles.SetActive(true);
        RightArmParticles.SetActive(true);
        AuraParticles.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaFighterInput : FighterInput
{
    // Start is called before the first frame update

    // Update is called once per frame

    public Attack LeftArmCollider;
    public Attack RightArmCollider;
    public Attack LeftLegCollider;
    public Attack RightLegCollider;

    public GameObject fireworks;
    public GameObject LeftArmParticles;
    public GameObject RightArmParticles;


    private void Awake()
    {
        LeftArmParticles.SetActive(false);
        RightArmParticles.SetActive(false);
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
            LeftArmParticles.SetActive(true);
            RightArmParticles.SetActive(true);
            _fighter.special = 0;
            changeDamageMod(2.0f);
            _animator.SetTrigger("Special");

            if (fireworks != null)
            {
                Instantiate(fireworks, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
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
        LeftArmParticles.SetActive(false);
        RightArmParticles.SetActive(false);
        changeDamageMod(1.0f);
    }
}

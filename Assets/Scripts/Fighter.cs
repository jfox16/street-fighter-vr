using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    public float health = 1000000;
    public float special = 0;
    public float specialCost = 10;
    public int fighterid;
    public float damageModifier = 1.0f;

    Animator animator;
    void Awake() {
        animator = GetComponent<Animator>();
        //flash = GameObject.Find("Flash").GetComponent<FlashScript>();
        
    }
    public override void Hurt(float damage) {
        if (animator.GetBool("Block"))
        {
            health -= damage * 0.5f;
        }
        else
        {
            animator.SetTrigger("Hurt");
            health -= damage;
         //   flash.PlayerHit();
        }
        Debug.Log(health);
        if (health < 0)
        {
            health = 0;
        }
        if (health <= 0) Die();
    }
    void Die() {
        animator.SetTrigger("Die");
    }
    public void ResetFighterHealth()
    {
        health = 100;
        animator.SetTrigger("Idle");
    }
    public void gainSpecial(float val)
    {
        special += val;
        if(special > 100f)
        {
            special = 100f;
        }else if ( special < 0)
        {
            special = 0;
        }
    }
}

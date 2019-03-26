using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsHealth : Unit
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Hurt(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            breakPart();
        }
    }

    void breakPart()
    {
        Rigidbody t = this.gameObject.AddComponent<Rigidbody>();
        t.mass = 0.1f;
    }
}

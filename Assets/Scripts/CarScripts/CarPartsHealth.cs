using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsHealth : Unit
{
    public float health;
    private GameObject g, g1;
    private Destruction s;
    private bool isBroken;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("CarParts");
        g1 = GameObject.Find("Car");
        s = g1.gameObject.GetComponent<Destruction>();
        isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        // delete later
        if(health > 0 && !isBroken)
        {
            Hurt(1.0f);
            s.setHealth(s.getHealth() - 1.0f);
        }
    }

    public override void Hurt(float damage)
    {
        health -= damage;
        if(health <= 0 && !isBroken)
        {
            breakPart();
        }
    }

    void breakPart()
    {
        float randomDir = Random.Range(-1.0f, 1.0f);
        Rigidbody t = this.gameObject.AddComponent<Rigidbody>();
        t.mass = 0.1f;
        t.AddForce((transform.forward + transform.up) * 3.0f * randomDir);
        s.setScore(s.getScore() + 50);
    }

    public void setBroken(bool isBroken)
    {
        this.isBroken = isBroken;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsHealth : Unit
{
    public float health;
    public int points;
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
        /*if(health > 0 && !isBroken)
        {
            Hurt(1.0f);
            s.setHealth(s.getHealth() - 1.0f);
        }*/
    }

    public override void Hurt(float damage)
    {
        health -= damage;
        //Debug.Log(health);
        if(health <= 0 && !isBroken)
        {
            isBroken = true;
            breakPart();
        }
    }

    void breakPart()
    {
        float randomDir = Random.Range(-1.0f, 1.0f);
        Rigidbody t = this.gameObject.GetComponent<Rigidbody>();
        t.isKinematic = false;
        t.mass = 0.1f;
        t.AddForce((transform.forward + transform.up) * 3.0f * randomDir);
        s.setScore(s.getScore() + points);
    }

    public void setBroken(bool isBroken)
    {
        this.isBroken = isBroken;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBroken)
        {
            Hurt(5.0f);
            s.setHealth(s.getHealth() - 5.0f);
            Debug.Log(s.getHealth());
            //Debug.Log(health + " " + this.gameObject.ToString());
        }
    }
}

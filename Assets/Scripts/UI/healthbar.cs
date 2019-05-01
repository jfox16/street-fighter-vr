using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Fighter fighter;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(fighter.transform.position.x, fighter.transform.position.y+2, fighter.transform.position.z);
        //Debug.Log(gameObject.transform.position);

        //Debug.Log("health " + fighter.getHealth());
        float health = fighter.health / 100;
        //Debug.Log("health " + health);
        // gameObject.transform = new Vector2()
        GameObject greenHealth = transform.Find("Health").gameObject;
        greenHealth.transform.localScale = new Vector3(health, greenHealth.transform.localScale.y, greenHealth.transform.localScale.z);
    }
    public void setFighter(Fighter f)
    {
        fighter = f;
    }


}
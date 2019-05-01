using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Camera;
    public Fighter fighter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fighter == null) return;
        //Debug.Log("health " + fighter.getHealth());
        float health = fighter.health / 100;
        //Debug.Log("health " + health);
        // gameObject.transform = new Vector2()
        GameObject greenHealth = transform.Find("Health").gameObject;
        greenHealth.transform.localScale = new Vector3(health, greenHealth.transform.localScale.y, greenHealth.transform.localScale.z);
    }
    public void setCamera(Camera c)
    {
        Camera = c;
    }
    public void setFighter(Fighter f)
    {
        fighter = f;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Camera;
    public Fighter fighter;
    GameObject greenHealth;

    void Awake()
    {
        greenHealth = transform.Find("Health").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (fighter != null) {
            float health = fighter.health / 100;
            greenHealth.transform.localScale = new Vector3(health, greenHealth.transform.localScale.y, greenHealth.transform.localScale.z);
        }
    }
    public void setFighter(Fighter f)
    {
        fighter = f;
    }


}

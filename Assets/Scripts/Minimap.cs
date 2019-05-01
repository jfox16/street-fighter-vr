using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Fighter fighter;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(fighter.transform.position.x, fighter.transform.position.y + 50, fighter.transform.position.z);
       
    }
    public void setFighter(Fighter f)
    {
        fighter = f;
    }

}

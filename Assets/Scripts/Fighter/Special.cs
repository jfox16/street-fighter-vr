using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    // Start is called before the first frame update
    protected float cost;
    protected Fighter fighter;
    protected Special(float cost, Fighter fighter)
    {
        this.cost = cost;
        this.fighter = fighter;
    }
    void Start()
    {

    }
}

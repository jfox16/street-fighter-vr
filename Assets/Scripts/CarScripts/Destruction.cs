using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private Dictionary<string, GameObject> carParts;
    private CarParts s;
    private GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<CarParts>();
        carParts = s.getCarParts();
        //test = GameObject.Find("Classic_16_Door_L");
        test = carParts["doorL"];
    }

    private void OnMouseDown()
    {
        /*GameObject backWindow = carParts["windowB"];
        Rigidbody backWindowRB = backWindow.AddComponent<Rigidbody>();
        backWindowRB.mass = 0.1f;*/

        Rigidbody t = test.AddComponent<Rigidbody>();
        t.mass = 0.1f;
    }
}

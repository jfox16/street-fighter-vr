using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class PrefabController : MonoBehaviour
{
    public GameObject rightHand, leftHand, head;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
    }
}

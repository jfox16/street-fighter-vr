using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject player;
    Animator a;
     void Start()
    {
        a = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
    }


}

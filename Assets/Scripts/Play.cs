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
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    Animator a;
    private int check;
   // public GameObject punchBag;
    void Start()
    {
        a = GetComponent<Animator>();
        check = 0;
    }
    IEnumerator OnTriggerEnter(Collider other)
    { 
        a.Play("box_open", 0, .25f);
        yield return new WaitForSeconds(1.5f);
        if (check == 0)
        {
           // GameObject newPB = Instantiate(punchBag);
            check = 1;
        }
       


    }

    
}

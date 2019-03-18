using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanInput : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health = 100;

    [SerializeField] GameObject punchPrefab;
    [SerializeField] GameObject kickPrefab;

    Animator animator;
    FPLook fpLook;
    Transform punchPointTransform;
    Transform kickPointTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
        if (Input.GetButtonDown("Punch") || Input.GetMouseButtonDown(0))
        {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("Jab");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DummyMovement : MonoBehaviour
{
    [SerializeField] GameObject kickPrefab;
    private GameObject _player;
    Animator animator;
    Transform kickPointTransform;

    // Start is called before the first frame update
    void Start()
    {
        //getPlayer();
        animator = GetComponent<Animator>();
        kickPointTransform = transform.Find("KickPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            float dist = Vector3.Distance(transform.position, _player.transform.position);
            if (dist <= 2.0f)
            {
                animator.SetTrigger("Kick");
                Instantiate(kickPrefab, kickPointTransform);
                //Debug.Log(dist);
            }
        }
    }
}

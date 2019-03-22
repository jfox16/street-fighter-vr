using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
{
    [SerializeField] private float speed;

    public GameObject player;
    // Update is called once per frame
    private void Awake()
    {
    }
    void Update()
    {
        this.gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private void OnDestroy()
    {
    }
}

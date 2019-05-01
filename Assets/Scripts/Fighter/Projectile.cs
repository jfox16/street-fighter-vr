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
        Invoke("DestroyMe",2.0f);
    }
    void Update()
    {
        // delete rigidbody on ball
        gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Destroy(gameObject);
        CancelInvoke();
    }
    public void DestroyMe()
    {
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}

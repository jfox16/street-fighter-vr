using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To use a Projectile, Instantiate it, Initialize it, then Launch it in a direction.
public class Projectile : Attack
{
    [SerializeField] private float moveSpeed;
    [SerializeField] float lifeTime = 5.0f;
    Vector3 moveDir = Vector3.zero;
    Rigidbody rigidbody;

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rigidbody.velocity = moveDir * moveSpeed * Time.deltaTime * 100f;
    }

    public void Launch(Vector3 moveDir) {
        this.moveDir = Vector3.ClampMagnitude(moveDir, 1);
        Destroy(gameObject, lifeTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
{
    [SerializeField] private float moveSpeed;
    Vector3 moveDir = Vector3.zero;
    Rigidbody rigidbody;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rigidbody.velocity = moveDir * moveSpeed * Time.deltaTime * 100f;
    }

    public void Launch(Vector3 moveDir) {
        this.moveDir = Vector3.ClampMagnitude(moveDir, 1);
    }
}

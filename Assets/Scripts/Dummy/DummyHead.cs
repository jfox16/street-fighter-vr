using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHead : Hittable
{
    Dummy parentScript;

    void Awake() {
        parentScript = transform.parent.GetComponent<Dummy>();
    }

    public override void Hit(float damage) {
        parentScript.HitHead();
    }
}

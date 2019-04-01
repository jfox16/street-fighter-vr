using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    float damage = 1;

    void Update() {

    }

    void OnTriggerEnter(Collider collider) {
        Unit _unit = collider.GetComponent<Unit>();
        if (_unit != null) {
            _unit.Hurt(damage);
            //Debug.Log("Punching a " + collider.gameObject);
        }

        Hittable _hittable = collider.GetComponent<Hittable>();
        if (_hittable != null) {
            _hittable.Hit(damage);
            //Debug.Log("Punching a " + collider.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAttack : MonoBehaviour
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] float radius = 1.0f;

    void Start()
    {
        // Damage units in radius
        int _unitMask = LayerMask.GetMask("Player");
        Collider[] targetColliders = Physics.OverlapSphere(
            transform.position,
            radius,
            _unitMask
        );
        foreach (Collider collider in targetColliders)
        {
            Fighter _unit = collider.GetComponent<Fighter>();
            _unit.Hurt(damage);
            //Debug.Log("Hurt " + collider.gameObject.ToString() + " for " + damage + " damage!");
        }

    }

    void Die()
    {
        // Destroys self
        Destroy(gameObject);
    }
}

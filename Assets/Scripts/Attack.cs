using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float damage = 1;
    [SerializeField] float radius = 1;
    [SerializeField] GameObject debugHitboxPrefab;

    void Start() {
        // Damage units in radius
        int _unitMask = LayerMask.GetMask("Unit");
        Collider[] targetColliders = Physics.OverlapSphere(
            transform.position,
            radius,
            _unitMask
        );
        foreach(Collider collider in targetColliders) {
            Unit _unit = collider.GetComponent<Unit>();
            _unit.Hurt(damage);
            Debug.Log("Hurt " + collider.gameObject.ToString() + " for " + damage + " damage!");
        }

        // Creates a visual representation of the hitbox
        GameObject dhp = Instantiate(debugHitboxPrefab, transform);
        dhp.transform.localScale = new Vector3(1,1,1) * radius;

        // Disappear after 0.5 seconds
        Invoke("Die", 0.5f);
    }

    void Die() {
        // Destroys self
        Destroy(gameObject);
    }
}

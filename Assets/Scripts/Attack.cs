using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float damage = 1;
    [SerializeField] float radius = 1;
    [SerializeField] GameObject debugHitboxPrefab;
    [SerializeField] float dieTime;

    void Start() {
        // Damage units in radius
        int _unitMask = LayerMask.GetMask("Unit");
        Collider[] targetColliders = Physics.OverlapSphere(
            transform.position,
            radius,
            _unitMask
        );
        /*
        foreach (Collider collider in targetColliders) {
            Unit _unit = collider.GetComponent<Unit>();
            _unit.Hurt(damage);
            Debug.Log("Hurt " + collider.gameObject.ToString() + " for " + damage + " damage!");  
        }*/

        // Disappear after 0.5 seconds
        Destroy(gameObject, dieTime);
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        Unit _unit = other.gameObject.GetComponent<Unit>();
        // Creates a visual representation of the hitbox
        GameObject dhp = Instantiate(debugHitboxPrefab, transform);
        dhp.transform.localScale = new Vector3(1, 1, 1) * radius;
        if (other.gameObject.tag == ("Player"))
        {
            return;
        }
        else
        {
            if (_unit != null)
            {
                _unit.Hurt(damage);
                //Debug.Log("Hurt " + other.gameObject.ToString() + " for " + damage + " damage!");
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 1;
    [SerializeField] GameObject debugHitboxPrefab;
    [SerializeField] float dieTime;
    private int ownerID;
  
    void  Start() {
        // Destroy(gameObject, 2f);
        // null ptr when dummy attacks
        if (GetComponentInParent<Fighter>() != null)
        {
            ownerID = gameObject.GetComponentInParent<Fighter>().gameObject.GetInstanceID();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("Called in projectile");
        Unit _unit = other.gameObject.GetComponent<Unit>();
        // Creates a visual representation of the hitbox
        //GameObject dhp = Instantiate(debugHitboxPrefab, transform);
        //dhp.transform.localScale = new Vector3(1, 1, 1) * radius;
        if (_unit != null && _unit.gameObject.GetInstanceID() != ownerID)
        {
            _unit.Hurt(damage);
            //Debug.Log("Hurt " + other.gameObject.ToString() + " for " + damage + " damage!");
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}

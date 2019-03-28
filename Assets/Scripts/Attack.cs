using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float damage = 1;
    [SerializeField] GameObject debugHitboxPrefab;
    [SerializeField] float dieTime;

  
    void Start() {
        //Destroy(gameObject, 5f);
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        Unit _unit = other.gameObject.GetComponent<Unit>();
        // Creates a visual representation of the hitbox
        //GameObject dhp = Instantiate(debugHitboxPrefab, transform);
        //dhp.transform.localScale = new Vector3(1, 1, 1) * radius;
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
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}

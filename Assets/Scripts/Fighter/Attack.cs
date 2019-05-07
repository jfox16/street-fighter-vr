using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Attack should be attached to a GameObject with a Collider.
 * When it collides with a Unit child class, it checks to see
 * if its a valid target and calls its Hurt function.
 */
public class Attack : MonoBehaviour
{
    public float damage = 1;
    public int ownerID;
    public GameObject owner = null;
    public Collider collider;
    
    [SerializeField] GameObject sparkPrefab;
    [SerializeField] GameObject hitParticlePrefab;
    GameObject hitSpark;
    
    void Awake() {
        if (GetComponentInParent<Fighter>() != null)
        {
            ownerID = gameObject.GetComponentInParent<Fighter>().gameObject.GetInstanceID();
            owner = gameObject.GetComponentInParent<Fighter>().gameObject;
        }

        collider = GetComponent<Collider>();
        hitSpark = (GameObject)Resources.Load("Particles/HitSpark");
    }

    protected void OnTriggerEnter(Collider other)
    {
        Unit _unit = other.gameObject.GetComponent<Unit>();
        if (_unit != null && _unit.gameObject.GetInstanceID() != ownerID)
            {
            // Instantiate(hitParticlePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Instantiate(hitSpark, transform.position, transform.rotation);
                // fix a bug
                if(Time.timeSinceLevelLoad > 2)
                {
                if (sparkPrefab != null)
                    {
                        // Instantiate(sparkPrefab, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));
                    }
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Punch", owner.transform.position);
                    _unit.Hurt(damage);
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
    }
}

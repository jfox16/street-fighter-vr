using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 1;
    [SerializeField] GameObject sparkPrefab;
    [SerializeField] GameObject hitParticlePrefab;
    public int ownerID;
    public GameObject owner = null;
    
    void Awake() {
        // Destroy(gameObject, 2f);
        // null ptr when dummy attacks
        if (GetComponentInParent<Fighter>() != null)
        {
            ownerID = gameObject.GetComponentInParent<Fighter>().gameObject.GetInstanceID();
            owner = gameObject.GetComponentInParent<Fighter>().gameObject;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Unit _unit = other.gameObject.GetComponent<Unit>();
        if (_unit != null && _unit.gameObject.GetInstanceID() != ownerID)
            {
            Instantiate(hitParticlePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
                // fix a bug
                if(Time.timeSinceLevelLoad > 2)
                {
                if (sparkPrefab != null)
                    {
                        Instantiate(sparkPrefab, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));
                    }
                    Debug.Log(this.gameObject.name);
                    _unit.Hurt(damage);
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        if(other.gameObject != owner)
        {
            if (this.gameObject.name.Equals("LeftArmCollider"))
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Punch", owner.transform.position);
            else if (this.gameObject.name.Equals("RightLegCollider"))
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Kick", owner.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 20;
    //[SerializeField] GameObject debugHitboxPrefab;
    [SerializeField] GameObject sparkPrefab;
    [SerializeField] float dieTime;
    private int ownerID;
  
    void Awake() {
        // Destroy(gameObject, 2f);
        // null ptr when dummy attacks
        if (GetComponentInParent<Fighter>() != null)
        {
            ownerID = gameObject.GetComponentInParent<Fighter>().gameObject.GetInstanceID();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Unit _unit = other.gameObject.GetComponent<Unit>();
        // Creates a visual representation of the hitbox
        //GameObject dhp = Instantiate(debugHitboxPrefab, transform);
        //dhp.transform.localScale = new Vector3(1, 1, 1) * radius;
        //Debug.Log("Hurt " + other.gameObject.ToString() + " for " + damage + " damage!");
        if (_unit != null && _unit.gameObject.GetInstanceID() != ownerID)
            {
                // fix a bug
                if(Time.timeSinceLevelLoad > 2)
                {
                    Instantiate(sparkPrefab, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Punch", gameObject.GetComponentInParent<Fighter>().gameObject.transform.position);
                    _unit.Hurt(damage);
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
    }
}

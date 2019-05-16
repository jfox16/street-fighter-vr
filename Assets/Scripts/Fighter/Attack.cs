using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * Attack should be attached to a GameObject with a Collider.
 * When it collides with a Unit child class, it checks to see
 * if its a valid target and calls its Hurt function.
 */
public class Attack : MonoBehaviour
{
    public Unit.Team ownerTeam;
    public float damage = 1;
    public Collider collider;
    
    [SerializeField] GameObject sparkPrefab;
    [SerializeField] GameObject hitParticlePrefab;
    GameObject hitSpark;
    private bool hitMiss = true;
    
    protected void Awake() {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        hitSpark = (GameObject)Resources.Load("Particles/HitSpark");
    }

    protected void OnTriggerEnter(Collider other)
    {
        hitMiss = false;
        Unit _unit = other.gameObject.GetComponent<Unit>();
        if (_unit != null && _unit.team != ownerTeam)
        {
            Debug.Log("Hit a Unit for " + damage + " damage!");
            _unit.Hurt(damage);
            _unit.MakeHitParticle(transform.position);
            _unit.PlayHitSound(transform.position);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    public void PlayWoosh()
    {
        if (hitMiss)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Whoosh", this.gameObject.transform.position);
        }
        hitMiss = true;
    }

    // void MakeHitSpark() {
    //     if (PhotonNetwork.CurrentRoom == null) {
    //         Instantiate(hitSpark, transform.position, transform.rotation);
    //     }
    //     else {
            
    //     }
    // }

    // void PlayHitSound() {
    //     if (this.gameObject.name.Equals("LeftArmCollider") || this.gameObject.name.Equals("RightArmCollider"))
    //         FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Punch", transform.position);
    //     else if (this.gameObject.name.Equals("RightLegCollider") || this.gameObject.name.Equals("RightLegCollider"))
    //         FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Kick", transform.position);
    // }
}

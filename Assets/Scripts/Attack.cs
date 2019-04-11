using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // ownerTeam must be set to use Attack
    public Unit.Team ownerTeam;
    [SerializeField] float damage = 1;
    
    new Collider collider;

    protected void Awake() {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    protected void OnTriggerEnter(Collider other) {
        Unit _unit = other.GetComponent<Unit>();

        Debug.Log("Hurting a Unit");
        if (_unit == null || _unit.team == ownerTeam) return;
        _unit.Hurt(damage);
        collider.enabled = false;
    } 

    public void SetEnabled(bool enabled) {
        collider.enabled = enabled;
    }
}

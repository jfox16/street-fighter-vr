using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float damage = 1;

    Unit.Team team = Unit.Team.Neutral;

    protected void Awake() {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Hittable _hittable = other.GetComponent<Hittable>();

        if (_hittable == null) return;

        /* Don't hit if other collider is a unit and it 
        is on the same team as Attack. */
        Unit _unit = other.GetComponent<Unit>();
        if (_unit != null && _unit.team == this.team) return;

        _hittable.Hit(damage);
        Destroy(gameObject);
    }

    public void Initialize(Unit.Team team) {
        this.team = team;
        gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{

    Collider collider;
    ParticleSystem firePS, ballPS;

    bool isCharging = false;
    public float chargeLevel = 0;
    [SerializeField] float chargeRate = 0.1f;



    protected new void Awake() {
        base.Awake();
        collider = GetComponent<Collider>();
        firePS = transform.Find("Fire").GetComponent<ParticleSystem>();
        ballPS = transform.Find("Ball").GetComponent<ParticleSystem>();
    }

    void Update() {
        if (isCharging) {
            chargeLevel += chargeRate*Time.deltaTime;
            if (chargeLevel > 1) chargeLevel = 1;
        }

        UpdateParticleSystems();
    }

    protected new void OnTriggerEnter(Collider other)
    {
        if (chargeLevel > 0.1f) {
            base.OnTriggerEnter(other);
        }
    }



    void UpdateParticleSystems() {
        var _fMain = firePS.main;
        var _fEmission = firePS.emission;
        var _bMain = ballPS.main;

        _fMain.startSize = 0.2f * chargeLevel;
        _fEmission.rateOverTime = 35 * chargeLevel;
        _fEmission.rateOverDistance = 20 * chargeLevel;
        _bMain.startSize = 0.4f * chargeLevel;
    }

    public void SetCharging(bool isCharging) {
        this.isCharging = isCharging;
    }
}

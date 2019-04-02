using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHand : MonoBehaviour
{



    [HideInInspector] public Unit.Team team;

    public enum Hand {Left, Right}
    public Hand hand;

    /* powerVec represents a punch's attack and direction.
    Magnitude is always between 0 and 1. */
    Vector3 powerVec = Vector3.zero;

    /* lasPos is the last position of this PlayerHand. 
    Used for calculating powerVec. */
    Vector3 lastPos;

    /* handRenderers is a List of mesh renderers in the
    different parts of the hand. */
    List<Renderer> handRenderers = new List<Renderer>();

    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Vector3 fireballDirection;
    Transform fireballPointTransform;
    Fireball fireball = null; // Current charging fireball
    bool isCharging = false;





    #region UNITY CALLBACKS

    void Awake() {
        /* Keep track of hand renderers so we can change their
        color later */
        foreach (Renderer _r in transform.Find("Model").GetComponentsInChildren<Renderer>()) {
            handRenderers.Add(_r);
        }

        fireballPointTransform = transform.Find("Fireball Point");
        // Initialize lastPos
        lastPos = transform.position;
    }

    void Update() {
        UpdatePowerVec();
        Charge();
        SetHandColor();

        lastPos = transform.position;
    }

    // OnTriggerEnter is just punching logic. Will only punch if you're punching fast enough.
    void OnTriggerEnter(Collider other) {
        int _damage = (int) (powerVec.magnitude * 10);
        // Debug.Log("PUNCH POWER: " + powerVec.magnitude);

        if (powerVec.magnitude > 0.5f) {
            Hittable _hittable = other.GetComponent<Hittable>();

            if (_hittable == null) return;

            /* Don't hit if other collider is a unit and it 
            is on the same team as Attack. */
            Unit _unit = other.GetComponent<Unit>();
            if (_unit != null && _unit.team == this.team) return;

            _hittable.Hit(_damage);

            // Rumble on hit
            if (hand == Hand.Left) {
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(1, 0.8f, 0.2f);
            }
            else {
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(1, 0.8f, 0.2f);
            }
        }
    }

    #endregion





    #region PRIVATE METHODS

    /* UpdatePowerVec calculates a new powerVec based on the 
    distance and direction traversed by PlayerHand this frame. */
    void UpdatePowerVec() {
        Vector3 _diffVec = (lastPos-transform.position)*Time.deltaTime*1800; // Distance PlayerHand traveled this frame
        _diffVec = Vector3.ClampMagnitude(_diffVec, 1); // Keep diffVec between 0 and 1
        powerVec = (powerVec + _diffVec) * 0.5f; // Average powerVec and diffVec
    }

    void Charge() {
        if (isCharging) {
            if (fireball == null) {
                fireball = Instantiate(fireballPrefab, fireballPointTransform).GetComponent<Fireball>();
                fireball.Initialize(team);
            }
            fireball.SetCharging(true);
            // Rumble when charging
            if (hand == Hand.Left) {
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(0, 0.2f+fireball.chargeLevel*0.4f, 0.1f);
            }
            else {
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(0, 0.2f+fireball.chargeLevel*0.4f, 0.1f);
            }
        }
        else {
            if (fireball != null) {
                fireball.SetCharging(false);
                fireball.transform.parent = null;
                fireball.Launch(transform.rotation * fireballDirection);
                fireball = null;
            }
        }
    }
    
    /* SetHandColor() sets the hand renderers' color depending
    on power. Min power is white, max power is red. */
    void SetHandColor() {
        foreach (Renderer _r in handRenderers) {
            float _g = 1 - powerVec.magnitude*0.9f;
            float _b = 1 - powerVec.magnitude;
            _r.material.color = new Color(1, _g, _b);
        }
    }

    #endregion





    #region PUBLIC METHODS

    public void SetCharging(bool isCharging) {
        this.isCharging = isCharging;
    }

    #endregion
}

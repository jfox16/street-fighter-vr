using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHand : MonoBehaviour
{
    //=================================================================================================================

    #region INSTANCE VARIABLES

    [HideInInspector] public Unit.Team team;

    public enum Hand {Left, Right}
    public Hand hand;

    /* punchPowerVec represents a punch's attack and direction.
    Magnitude is always between 0 and 1. */
    Vector3 punchPowerVec = Vector3.zero;

    /* lastPos is the position of this object last frame. Used for calculating punchPowerVec. */
    Vector3 lastPos;

    List<Renderer> handRenderers = new List<Renderer>();

    /* punchThreshold determines the speed necessary to trigger a punch. (float value between 0 and 1) */
    [SerializeField] float punchThreshold = 0.5f;
    /* Higher punchDistanceModifier means player must punch further to trigger a punch. */
    [SerializeField] float punchDistanceModifier = 1;

    #endregion

    //=================================================================================================================

    #region UNITY CALLBACKS

    void Awake() {
        /* Keep track of mesh renderers on hands so we can change their
        color later. */
        foreach (Renderer _r in transform.Find("Model").GetComponentsInChildren<Renderer>()) {
            handRenderers.Add(_r);
        }

        lastPos = transform.localPosition; // initialize lastPos
    }

    void Update() {
        UpdatePowerVec();
        SetHandColor();

        // Debug.Log("PUNCH POWER: " + punchPowerVec.magnitude);
        if (punchPowerVec.magnitude > punchThreshold
        && Vector3.Angle(punchPowerVec, transform.localPosition) > 90) {
            // Debug.Log("PUNCHHHHHHHHHHHHH!");
            VRInputHandler.InputPunch(hand, punchPowerVec);
            punchPowerVec = Vector3.zero;
        }
    }

    void LateUpdate() {
        lastPos = transform.localPosition;
    }

    void OnTriggerEnter(Collider other) {
        int _damage = (int) (punchPowerVec.magnitude * 10);

        if (punchPowerVec.magnitude > 0.5f) {

            // Check if other is a Unit
            Unit _unit = other.GetComponent<Unit>();
            if (_unit == null || _unit.team == this.team) return;

            _unit.Hurt(_damage);

            // Rumble on hit
        }
    }

    #endregion

    //=================================================================================================================

    #region PRIVATE METHODS

    /* UpdatePowerVec calculates a new punchPowerVec based on the 
    distance and direction traversed by PlayerHand this frame. */
    void UpdatePowerVec() {
        // diffVec is the distance PlayerHand traveled this frame
        Vector3 _diffVec = (lastPos-transform.localPosition)*Time.deltaTime*1500; 
        _diffVec = Vector3.ClampMagnitude(_diffVec, 1); // Keep diffVec between 0 and 1
        /* Average punchPowerVec and _diffVec, but weight punchPowerVec with punchDistanceModifier. 
        The higher punchDistanceModifier is, the more the average will be skewed towards the current punchPowerVec. */
        punchPowerVec = (punchPowerVec*punchDistanceModifier + _diffVec) / (1 + punchDistanceModifier);
    }
    
    /* SetHandColor() sets the hand renderers' color depending on power. Min power is white, max power is red. */
    void SetHandColor() {
        foreach (Renderer _r in handRenderers) {
            float _g = 1 - punchPowerVec.magnitude*0.9f;
            float _b = 1 - punchPowerVec.magnitude;
            _r.material.color = new Color(1, _g, _b);
        }
    }

    #endregion

    //=================================================================================================================
}

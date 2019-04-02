using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/* VRFighter acts as a controller for the two VRHands. */
public class VRFighter : Unit
{
    [SerializeField] VRHand leftHand, rightHand;

    float health = 100;

    void Awake() {
        team = Team.Red;
        leftHand.team = team;
        rightHand.team = team;
    }

    void Update() {
        leftHand.SetCharging(Input.GetButton("X"));
        rightHand.SetCharging(Input.GetButton("A"));
        if (Input.GetButton("X")) {
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(0, 0.5f, 0.1f);
        }
        if (Input.GetButton("A")) {
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(0, 0.5f, 0.1f);
        }
    }

    public override void Hit(float damage) {
        health -= damage;
    }
}

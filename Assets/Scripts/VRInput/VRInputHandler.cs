using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class VRInputHandler : MonoBehaviour
{
    static VRInputHandler Instance = null; 

    Dictionary<string, bool> inputDict = new Dictionary<string, bool>();

    void Awake() {
        Instance = this;

        // Initialize inputDict
        inputDict.Add("Left Punch", false);
        inputDict.Add("Right Punch", false);
        inputDict.Add("Right Smash", false);
    }

    void LateUpdate() {
        // Set all inputs to false at the end of the frame
        foreach (var key in inputDict.Keys.ToList()) {
            inputDict[key] = false;
        }
    }

    public static void InputPunch(VRHand.Hand hand, Vector3 dirVec) {
        if (hand == VRHand.Hand.Right) {
            SendHapticImpulse(VRHand.Hand.Right, 0.1f, 0.1f);
            if (Input.GetButton("B")) 
                Instance.inputDict["Right Smash"] = true;
            else 
                Instance.inputDict["Right Punch"] = true;
        }
        else {
            SendHapticImpulse(VRHand.Hand.Left, 0.1f, 0.1f);
            Instance.inputDict["Left Punch"] = true;
        }
    }

    public static bool GetInput(string inputKey) {
        if (Instance == null) return false;
        return Instance.inputDict[inputKey];
    }

    public static void SendHapticImpulse(VRHand.Hand hand, float intensity, float duration) {
        if (hand == VRHand.Hand.Left)
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(1, intensity, duration);
        else
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(1, intensity, duration);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRInputHandler : MonoBehaviour
{
    public static VRInputHandler Instance = null; 

    Dictionary<string, bool> inputDict = new Dictionary<string, bool>();

    void Awake() {
        if (Instance == null) {
            // Set this as Instance and keep it from being destroyed across scenes.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            // If an instance of this already exists, destroy this one.
            Destroy(gameObject);
        }

        // Initialize inputDict
        inputDict.Add("Left Punch", false);
        inputDict.Add("Right Punch", false);
        inputDict.Add("Right Smash", false);
        inputDict.Add("Kick", false);
        inputDict.Add("Special",false);

    }

    void Update() {
        // Secret input that restarts the game
        bool secretInputVR =   Input.GetAxisRaw("Left Index Trigger") > 0.5f
                            && Input.GetAxisRaw("Right Index Trigger") > 0.5f
                            && Input.GetButton("B")
                            && Input.GetButton("Y");
                            
        bool secretInputKB =   Input.GetKey(KeyCode.LeftControl) 
                            && Input.GetKey(KeyCode.Escape);

        if (secretInputVR || secretInputKB)
            SceneManager.LoadScene("VR Start");

        // Check special
        if ( Input.GetAxisRaw("Left Index Trigger") > 0.5f
            && Input.GetAxisRaw("Right Index Trigger") > 0.5f )
        {
            Instance.inputDict["Special"] = true;
        }
    }

    void LateUpdate() {
        // Set all inputs to false at the end of each frame
        foreach (var key in inputDict.Keys.ToList()) {
            inputDict[key] = false;
        }
    }

    public static void InputPunch(VRHand.Hand hand, Vector3 dirVec) 
    {
        if (hand == VRHand.Hand.Right) {
            SendHapticImpulse(VRHand.Hand.Right, 0.5f, 0.5f);
            if (Input.GetButton("B")) 
                Instance.inputDict["Right Smash"] = true;
            else 
                Instance.inputDict["Right Punch"] = true;
        }
        else {
            SendHapticImpulse(VRHand.Hand.Left, 0.5f, 0.5f);
            if (Input.GetButton("Y")) 
                Instance.inputDict["Kick"] = true;
            else 
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

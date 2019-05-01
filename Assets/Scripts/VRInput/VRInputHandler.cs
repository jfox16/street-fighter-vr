using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VRInputHandler : MonoBehaviour
{
    static VRInputHandler Instance = null; 

    Dictionary<string, bool> inputDict = new Dictionary<string, bool>();

    void Awake() {
        Instance = this;

        // Initialize inputDict
        inputDict.Add("Punch Forward", false);
    }

    void LateUpdate() {
        // Set all inputs to false at the end of the frame
        foreach (var key in inputDict.Keys.ToList()) {
            inputDict[key] = false;
        }
    }

    public static void InputPunch(Vector3 dirVec) {
        Instance.inputDict["Punch Forward"] = true;
    }

    public static bool GetInput(string inputKey) {
        return Instance.inputDict[inputKey];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    void Update() {
        transform.LocalPosition - OVRInput.getLocalControllerPosition(OVRInput.Controller.RTouch);
        transform.LocalPosition - OVRInput.getLocalControllerPosition(OVRInput.Controller.RTouch);
        transform.LocalPosition - OVRInput.getLocalControllerPosition(OVRInput.Controller.LTouch);
        transform.LocalPosition - OVRInput.getLocalControllerPosition(OVRInput.Controller.LTouch);
    }
}

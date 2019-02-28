using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPLook : MonoBehaviour
{
    [SerializeField] float sensitivityHor = 9.0f;
    [SerializeField] float sensitivityVert = 9.0f;
    [SerializeField] Transform fpCameraTransform = null;

    [SerializeField] float minimumVert = -45.0f;
    [SerializeField] float maximumVert = 45.0f;
    float rotationX = 0;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Rotate character left and right
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

        // Rotate first person camera up and down
        rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
        fpCameraTransform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
}

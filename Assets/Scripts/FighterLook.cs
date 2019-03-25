using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterLook : MonoBehaviour
{
    Transform fpCameraAttachPoint = null;
    Transform fpCameraTransform = null;

    [SerializeField] float sensitivityHor = 9.0f;
    [SerializeField] float sensitivityVert = 9.0f;

    [SerializeField] float minimumVert = -45.0f;
    [SerializeField] float maximumVert = 45.0f;

    float rotationX = 0;



    #region UNITY CALLBACKS

    protected void Awake() {
        fpCameraAttachPoint = transform.Find("FP Camera Attach Point");
    }

    protected void Update() {
        ReadInput();
    }

    #endregion



    protected void ReadInput() {
        if (fpCameraTransform != null) {
            // Rotate character left and right
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

            // Rotate first person camera up and down
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
            fpCameraTransform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }
    }



    #region PUBLIC METHODS

    // Attach a camera to this Fighter for first person view.
    // Returns true if successful, otherwise false.
    public bool AttachCamera(GameObject camera) {
        if (fpCameraTransform == null) {
            // set as new camera
            fpCameraTransform = camera.transform;
            fpCameraTransform.SetParent(fpCameraAttachPoint);
            // reset transform values
            fpCameraTransform.localPosition = Vector3.zero;
            fpCameraTransform.localRotation = Quaternion.identity;
            Cursor.lockState = CursorLockMode.Confined;
            return true;
        }
        else
            return false;
    }

    // Remove the first person camera from this fighter.
    // Returns true if successful, otherwise false.
    public bool RemoveCamera() {
        if (fpCameraTransform == null) {
            fpCameraTransform = null;
            Cursor.lockState = CursorLockMode.None;
            return true;
        }
        else 
            return false;
    }

    #endregion
}

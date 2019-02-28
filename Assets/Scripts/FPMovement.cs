using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float moveSpeed = 10;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        // Read input to _inputVec.
        // _inputVec is a directional vector with a length of 1.
        float _xInput = Input.GetAxis("Horizontal");
        float _yInput = Input.GetAxis("Vertical");
        Vector3 _inputVec = new Vector3(_xInput, 0, _yInput).normalized;

        // Get _moveVec by multiplying by moveSpeed and deltaTime.
        Vector3 _moveVec = _inputVec * moveSpeed * Time.deltaTime;
        // Rotate _moveVec by the rotation of the transform.
        // This will make movement relative to the direction the character is facing.
        _moveVec = transform.rotation * _moveVec;

        controller.Move(_moveVec);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FighterMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    CharacterController controller;
    // inputVec is a unit vector containing input direction
    Vector3 inputVec = Vector3.zero; 
    // moveVec is the distance and direction that Fighter will move this frame.
    public  Vector3 moveVec = Vector3.zero;

    protected void Awake() {
        controller = GetComponent<CharacterController>();
    }

    protected void FixedUpdate() {
        ReadInput();
        Move();
    }

    protected void ReadInput() {
        // Read input to _inputVec.
        // inputVec is a directional vector with a length of 1.
        float _xInput = Input.GetAxis("Horizontal");
        float _yInput = Input.GetAxis("Vertical");
        inputVec = Vector3.ClampMagnitude(new Vector3(_xInput, 0, _yInput), 1);
    }

    protected void Move() {
        moveVec = inputVec * moveSpeed * Time.deltaTime;
        moveVec = transform.rotation * moveVec;
        controller.Move(moveVec);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class FighterVRMovement : MonoBehaviour
{
    GameObject moveCursorPrefab;
    [SerializeField] float maxMoveRange = 2;
    [SerializeField] float moveCursorSpeed = 0.2f;

    Rigidbody rigidbody = null;
    CapsuleCollider collider = null;
    Animator animator = null;
    PhotonView photonView = null;
    
    // This is the cursor that gets spawned when moving
    GameObject moveCursor = null;

    // turnDelay determines how frequently to turn when turn is held
    [SerializeField] float turnDelay = 1;
    Timer turnTimer = new Timer();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        moveCursorPrefab = (GameObject)Resources.Load("Particles/MoveCursor");
    }

    void Update() 
    {
         // Only read input if this Fighter belongs to the client.
        if (!PhotonNetwork.IsConnected || photonView.IsMine) {
            ReadMoveInput();
            ReadTurnInput();
        }
    }

    void ReadMoveInput() {
        Vector3 _keyboardMoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 _controllerMoveInput = new Vector3(Input.GetAxisRaw("Left Horizontal"), 0, Input.GetAxisRaw("Left Vertical"));
        Vector3 _moveInput = Vector3.ClampMagnitude(_keyboardMoveInput + _controllerMoveInput, 1);

        if (_moveInput.magnitude > 0.05f)
        {
            // Move the move cursor in direction of move input.
            if (moveCursor == null) 
                moveCursor = Instantiate(moveCursorPrefab, transform);

            moveCursor.transform.Translate(_moveInput * moveCursorSpeed);
            moveCursor.transform.localPosition = Vector3.ClampMagnitude(moveCursor.transform.localPosition, maxMoveRange);
        }
        else 
        {
            // When move input is let go, move to that location.
            if (moveCursor != null) {
                // The SphereCast checks for any collisions between Fighter and the moveCursor point.
                // If a collision is found, Fighter will just move to the collision point.
                // Otherwise, move to moveCursor point.
                RaycastHit _hit;
                bool _wasHit = Physics.SphereCast(
                    transform.position + new Vector3(0, collider.radius+0.1f, 0), 
                    collider.radius, 
                    moveCursor.transform.position - transform.position,
                    out _hit, 
                    (moveCursor.transform.position - transform.position).magnitude
                );

                if (_wasHit) {
                    rigidbody.MovePosition(_hit.point - new Vector3(0, collider.radius+0.1f, 0));
                }
                else {
                    rigidbody.MovePosition(moveCursor.transform.position);
                }

                animator.SetTrigger("Dash");
                Destroy(moveCursor);
                moveCursor = null;
            }
        }
    }

    void ReadTurnInput() {
        float _keyboardTurnInput = -Input.GetAxisRaw("Mouse X");
        float _controllerTurnInput = Input.GetAxisRaw("Right Horizontal");
        float _turnInput = _keyboardTurnInput + _controllerTurnInput;

        if (Math.Abs(_turnInput) > 0.8f) {
            if (turnTimer.isDone) {
                transform.Rotate(0, -45*Mathf.Sign(_turnInput), 0);
                turnTimer.SetTime(turnDelay);
            }
        }
        else {
            turnTimer.SetTime(0);
        }
    }
}

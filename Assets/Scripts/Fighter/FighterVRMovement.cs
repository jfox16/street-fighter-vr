using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class FighterVRMovement : MonoBehaviour
{
    [SerializeField] float maxMoveRange = 2;
    [SerializeField] float moveCursorSpeed = 0.2f;

    Fighter fighter;
    Rigidbody rigidbody;
    CapsuleCollider collider;
    Animator animator;
    PhotonView photonView;
    
    // This is the cursor that gets spawned when moving
    GameObject moveCursor = null;

    // turnDelay determines how frequently to turn when turn is held
    [SerializeField] float turnDelay = 1;
    Timer turnTimer = new Timer();

    void Awake()
    {
        fighter    = GetComponent<Fighter>();
        rigidbody  = GetComponent<Rigidbody>();
        collider   = GetComponent<CapsuleCollider>();
        animator   = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();

        GameObject _moveCursorPrefab = (GameObject)Resources.Load("Particles/MoveCursor");
        moveCursor = Instantiate(_moveCursorPrefab, transform);
        moveCursor.SetActive(false);
    }

    void Update() 
    {
         // Only read input if this Fighter belongs to the client.
        if (fighter.isMine && fighter.isAlive) {
            ReadMoveInput();
            ReadTurnInput();
        }
    }

    void ReadMoveInput() {
        Vector3 _keyboardMoveInput  = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 _controllerMoveInput = new Vector3(Input.GetAxisRaw("Left Horizontal"), 0, Input.GetAxisRaw("Left Vertical"));
        Vector3 _moveInput = Vector3.ClampMagnitude(_keyboardMoveInput + _controllerMoveInput, 1);

        if (!animator.GetBool("isAttacking") && _moveInput.magnitude > 0.05f)
        {
            // Move the move cursor in direction of move input.
            if (!moveCursor.active) {
                moveCursor.SetActive(true);
                moveCursor.transform.localPosition = Vector3.zero;
            }

            moveCursor.transform.Translate(_moveInput * moveCursorSpeed * Time.deltaTime * 10);
            moveCursor.transform.localPosition = Vector3.ClampMagnitude(moveCursor.transform.localPosition, maxMoveRange);
        }
        else 
        {
            // When move input is let go, move to that location.
            if (moveCursor.active) {
                // The SphereCast checks for any collisions between Fighter and the moveCursor point.
                // If a collision is found, Fighter will just move to the collision point.
                // Otherwise, move to moveCursor point.
                RaycastHit _rcHit;
                bool _colliderHit = Physics.SphereCast(
                    transform.position + new Vector3(0, collider.radius+0.1f, 0), 
                    collider.radius, 
                    moveCursor.transform.position - transform.position,
                    out _rcHit, 
                    (moveCursor.transform.position - transform.position).magnitude
                );

                if (_colliderHit) {
                    rigidbody.MovePosition(_rcHit.point - new Vector3(0, collider.radius+0.1f, 0));
                }
                else {
                    rigidbody.MovePosition(moveCursor.transform.position);
                }

                animator.SetTrigger("Dash");
                moveCursor.SetActive(false);
            }
        }
    }

    void ReadTurnInput() {
        float _keyboardTurnInput = Input.GetAxisRaw("Mouse X") + Input.GetAxisRaw("Turn");
        float _controllerTurnInput = -Input.GetAxisRaw("Right Horizontal");
        float _turnInput = _keyboardTurnInput + _controllerTurnInput;

        if (!animator.GetBool("isAttacking") && Math.Abs(_turnInput) > 0.8f) {
            if (turnTimer.isDone) {
                transform.Rotate(0, 45*Mathf.Sign(_turnInput), 0);
                turnTimer.SetTime(turnDelay);
            }
        }
        else {
            turnTimer.SetTime(0);
        }
    }
}

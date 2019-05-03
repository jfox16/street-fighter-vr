using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FighterVRMovement : MonoBehaviour
{
    [SerializeField] GameObject moveCursorPrefab;
    [SerializeField] float maxMoveRange = 2;
    [SerializeField] float moveCursorSpeed = 0.2f;

    GameObject moveCursor = null;
    Rigidbody rigidbody = null;
    Animator animator = null;

    [SerializeField] float turnDelay = 1;
    Timer turnTimer = new Timer();

    void Update() 
    {
        rigidbody = GetComponent<Rigidbody>();
        ReadMoveInput();
        ReadTurnInput();
    }

    void ReadMoveInput() {
        Vector3 _keyboardMoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 _controllerMoveInput = new Vector3(Input.GetAxisRaw("Left Horizontal"), 0, Input.GetAxisRaw("Left Vertical"));
        Vector3 _moveInput = Vector3.ClampMagnitude(_keyboardMoveInput + _controllerMoveInput, 1);

        if (_moveInput.magnitude > 0.05f)
        {
            if (moveCursor == null) 
                moveCursor = Instantiate(moveCursorPrefab, transform);

            moveCursor.transform.Translate(_moveInput * moveCursorSpeed);
            moveCursor.transform.localPosition = Vector3.ClampMagnitude(moveCursor.transform.localPosition, maxMoveRange);
        }
        else 
        {
            if (moveCursor != null) {
                // transform.position = moveCursor.transform.position;
                rigidbody.MovePosition(moveCursor.transform.position);
                Destroy(moveCursor);
            }
        }
    }

    void ReadTurnInput() {
        float _keyboardTurnInput = Input.GetAxisRaw("Mouse X");
        float _controllerTurnInput = Input.GetAxisRaw("Right Horizontal");
        float _turnInput = _keyboardTurnInput + _controllerTurnInput;

        if (Math.Abs(_turnInput) > 0.4f) {
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

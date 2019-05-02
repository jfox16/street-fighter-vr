using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    private Animator _animator;
    bool canMove;
    [SerializeField] float moveSpeed = 2;
    private float epsilon = 0.000001f;
    FMOD.Studio.EventInstance footSteps;
    FMOD.Studio.PLAYBACK_STATE playbackState;

    void Awake() {
        _animator = GetComponent<Animator>();
        canMove = true;
        footSteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Characters/Mecha/Footsteps");
        //footSteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject.transform));
    }
    void FixedUpdate() {
        // Read input to _inputVec.
        // _inputVec is a directional vector with a length of 1.

        if (!_animator.GetBool("isAttacking"))
        {


            float _xInput = Input.GetAxis("Horizontal");
            float _yInput = Input.GetAxis("Vertical");
            Vector3 _inputVec = new Vector3(_xInput, 0, _yInput).normalized;

            // Get _moveVec by multiplying by moveSpeed and deltaTime.
            Vector3 _moveVec = _inputVec * moveSpeed * Time.deltaTime;
            // Rotate _moveVec by the rotation of the transform.
            // This will make movement relative to the direction the character is facing.
            _moveVec = transform.rotation * _moveVec;

            if (controller.velocity != Vector3.zero)
            {
                _animator.SetBool("isWalking", true);

                // check if the previous footstep sound finished
                //footSteps.getPlaybackState(out playbackState);
                //if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                //{
                //    footSteps.start();
                //}
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
            //_animator.SetFloat("Speed", _yInput);
            controller.Move(_moveVec);
        }
    }
}

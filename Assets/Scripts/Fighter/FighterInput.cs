using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FighterInput : MonoBehaviour
{
    // Start is called before the first frame update
    KeyCode LightPunch;
    KeyCode HeavyPunch;
    KeyCode Kick;
    KeyCode Special;
    KeyCode Dodge;
    KeyCode Block;
    KeyCode Crouch;

    public Animator _animator;
    protected Fighter _fighter;

    void Start()
    {
        _fighter = this.gameObject.GetComponent<Fighter>();
        LightPunch = KeyCode.Mouse0;
        HeavyPunch = KeyCode.Mouse1;
        Kick = KeyCode.LeftControl;
        Special = KeyCode.Space;
        Dodge = KeyCode.LeftShift;
        Block = KeyCode.C;
        Crouch = KeyCode.LeftShift;

    }

    // Update is called once per frame
    protected void Update()
    {
        if (!_animator.GetBool("isAttacking") && !_animator.GetBool("isWalking"))
        {
            if (VRInputHandler.GetInput("Right Smash")) {
                _animator.SetTrigger("Right Smash");
            }
            else if (VRInputHandler.GetInput("Right Punch")) {
                _animator.SetTrigger("Right Punch");
            }
            else if (VRInputHandler.GetInput("Left Punch")) {
                _animator.SetTrigger("Left Punch");
            }


            else if (Input.GetKeyDown(LightPunch))
            {
                punch();
            }
            else if (Input.GetKeyDown(Kick))
            {
                Debug.Log("kick");
                kick();
            }
            else if (Input.GetKeyDown(Block))
            {
                _animator.SetBool("Block", true);
            }
            else if (Input.GetKeyUp(Block))
            {
                _animator.SetBool("Block", false);
            }
            else if (Input.GetKeyDown(Special))
            {
                special();
            }
        }
    }

    public abstract void punch();
    public abstract void kick();
    public abstract void special();
}

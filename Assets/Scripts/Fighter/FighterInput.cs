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
        /*
        if (!_animator.GetBool("isAttacking"))
        {
            if (Input.GetKeyDown(LightPunch))
            {
                _animator.SetTrigger("Light_Punch");
               // Instantiate(lightPunchPrefab, lightPunchTransform);
            }

            if (Input.GetKeyDown(HeavyPunch))
            {
                _animator.SetTrigger("Heavy_Punch");
            }
            /*else if (Input.GetButtonDown("Punch Left"))
            {
                Instantiate(punchPrefab, punchPointTransform);
                animator.SetTrigger("PunchLeft");
            }*//*
            else if (Input.GetKeyDown(Kick))
            {
                //Instantiate(kickPrefab, kickPointTransform);
                _animator.SetTrigger("Kick");
            }*/
               /*else if (Input.GetButtonDown("Kick Left"))
               {
                   Instantiate(kickPrefab, kickPointTransform);
                   animator.SetTrigger("KickLeft");
               }*/

        /*
            else if (Input.GetKeyDown(Block))
            {
                _animator.SetBool("Block", true);
            }
            else if (Input.GetKeyUp(Block))
            {
                _animator.SetBool("Block", false);
            }
            else if (Input.GetKeyDown(Crouch))
            {
                _animator.SetBool("Crouch", true);
            }
            else if (Input.GetKeyUp(Crouch))
            {
                _animator.SetBool("Crouch", false);
            }
            else if (Input.GetKeyDown(Special) && timestamp < Time.time )
      
            {

                timestamp = Time.time + cooldown;
                _animator.SetTrigger("Special");
                Camera _camera;
                Ray _ray;
                Vector3 pointOffset, endPoint;
                GameObject controller = GameObject.Find("Game Controller");
                GameController g = controller.GetComponent<GameController>();
                _camera = g.getCamera();
                GameObject _ball;
                timestamp = Time.time + cooldown;
                        _ball = Instantiate(specialPrefab) as GameObject;
                        _ball.transform.position = lightPunchTransform.position;
                        _ball.transform.rotation = transform.rotation;
            }
        }
        */
        if (!_animator.GetBool("isAttacking"))
        {
            if (Input.GetKeyDown(LightPunch))
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

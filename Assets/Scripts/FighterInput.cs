using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterInput : MonoBehaviour
{
    // Start is called before the first frame update
    KeyCode LightPunch;
    KeyCode HeavyPunch;
    KeyCode Kick;
    KeyCode Special;
    KeyCode Dodge;
    KeyCode Block;
    KeyCode Crouch;

    [SerializeField] Animator _animator;

    [SerializeField] GameObject lightPunchPrefab;
    [SerializeField] GameObject HeavyPunchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject specialPrefab;

    [SerializeField] Transform lightPunchTransform;
    [SerializeField] Transform HeavyPunchTransform;
    [SerializeField] Transform SpecialTransform;

    [SerializeField] float cooldown;
    private float timestamp;

    bool isAttacking;
    void Start()
    {
        LightPunch = KeyCode.Mouse0;
        HeavyPunch = KeyCode.Mouse1;
        Kick = KeyCode.LeftControl;
        Special = KeyCode.Space;
        Dodge = KeyCode.LeftShift;
        Block = KeyCode.C;
        Crouch = KeyCode.LeftShift;

    }

    // Update is called once per frame
    void Update() {
        if (!_animator.GetBool("isAttacking"))
        {
            if (Input.GetKeyDown(LightPunch))
            {
                _animator.SetTrigger("Light_Punch");
                Instantiate(lightPunchPrefab, lightPunchTransform);
            }

            if (Input.GetKeyDown(HeavyPunch))
            {
                _animator.SetTrigger("Heavy_Punch");
            }
            /*else if (Input.GetButtonDown("Punch Left"))
            {
                Instantiate(punchPrefab, punchPointTransform);
                animator.SetTrigger("PunchLeft");
            }*/
            else if (Input.GetKeyDown(Kick))
            {
                //Instantiate(kickPrefab, kickPointTransform);
                _animator.SetTrigger("Kick");
            }
            /*else if (Input.GetButtonDown("Kick Left"))
            {
                Instantiate(kickPrefab, kickPointTransform);
                animator.SetTrigger("KickLeft");
            }*/
            else if (Input.GetKeyDown(Block))
            {
                Debug.Log("Blocoking");
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
            else if (Input.GetKeyDown(Special) && timestamp < Time.time)
            {
                timestamp = Time.time + cooldown;
                _animator.SetTrigger("Special");
                Invoke("SpawnProjectile", 0.3f);
            }
        }
    }
        
    void SpawnProjectile()
    {
        //problem: this will instantiate a new projectile point each time.
        Instantiate(specialPrefab, SpecialTransform.position, new Quaternion(0,0,0,0));
    }
}

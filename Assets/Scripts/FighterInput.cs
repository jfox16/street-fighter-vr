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

    [SerializeField] Animator animator;
    [SerializeField] GameObject lightPunchPrefab;
    [SerializeField] GameObject HeavyPunchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject specialPrefab;

    [SerializeField] Transform lightPunchTransform;
    [SerializeField] Transform HeavyPunchTransform;
    [SerializeField] Transform SpecialTransform;

    [SerializeField] float cooldown;
    private float timestamp;
    void Start()
    {
        LightPunch = KeyCode.Mouse0;
        HeavyPunch = KeyCode.Mouse1;
        Kick = KeyCode.LeftControl;
        Special = KeyCode.Space;
        Dodge = KeyCode.LeftShift;
        Block = KeyCode.C;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(Block))
        {
            animator.SetBool("Block", false);
        }
        if (Input.GetKeyDown(LightPunch))
        {
            animator.SetTrigger("Light_Punch");
            Instantiate(lightPunchPrefab, lightPunchTransform);
        }

        if (Input.GetKeyDown(HeavyPunch)) 
        {
            Instantiate(lightPunchPrefab, lightPunchTransform);
            animator.SetTrigger("Heavy_Punch");
        }
        /*else if (Input.GetButtonDown("Punch Left"))
        {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("PunchLeft");
        }*/
        else if (Input.GetKeyDown(Kick))
        {
            //Instantiate(kickPrefab, kickPointTransform);
            animator.SetTrigger("Kick");
        }
        /*else if (Input.GetButtonDown("Kick Left"))
        {
            Instantiate(kickPrefab, kickPointTransform);
            animator.SetTrigger("KickLeft");
        }*/
        else if (Input.GetKeyDown(Block))
        {
            animator.SetBool("Block", true);
        }
        else if (Input.GetKeyDown(Special) && timestamp < Time.time)
        {
            timestamp = Time.time + cooldown;
            animator.SetTrigger("Special");
            Invoke("SpawnProjectile", 0.3f);
        }

    }
    void SpawnProjectile()
    {
        Instantiate(specialPrefab, SpecialTransform.transform.position, new Quaternion(0, 0, 0, 0));
    }
}

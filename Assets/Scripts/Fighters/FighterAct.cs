using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAct : MonoBehaviour
{
    public enum State {Idle, Hurt, Attacking, Blocking, Crouching}
    public State currentState = State.Idle;

    Animator animator;

    [SerializeField] GameObject lightPunchPrefab;
    [SerializeField] GameObject HeavyPunchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject specialPrefab;

    [SerializeField] Transform lightPunchTransform;
    [SerializeField] Transform HeavyPunchTransform;
    [SerializeField] Transform SpecialTransform;

    [SerializeField] float specialCooldown;
    Timer specialTimer = new Timer();

    protected void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // I implemented these in the Input Manager
        // LightPunch = KeyCode.Mouse0;
        // HeavyPunch = KeyCode.Mouse1;
        // Kick = KeyCode.LeftControl;
        // Special = KeyCode.Space;
        // Dodge = KeyCode.LeftShift;
        // Block = KeyCode.C;
        // Crouch = KeyCode.LeftShift;

    }

    // Update is called once per frame
    protected void Update() {
       ReadInput();
    }

    protected void ReadInput() {
        if (currentState == State.Idle) {
            if (Input.GetButtonDown("Attack 1") || Input.GetMouseButtonDown(0)) {
                animator.SetTrigger("Attack 1");
                currentState = State.Attacking;
            }
            else if (Input.GetButtonDown("Attack 2") || Input.GetMouseButtonDown(1)) {
                animator.SetTrigger("Attack 2");
                currentState = State.Attacking;
            }
            /*else if (Input.GetButtonDown("Punch Left"))
            {
                Instantiate(punchPrefab, punchPointTransform);
                animator.SetTrigger("PunchLeft");
            }*/
            else if (Input.GetButtonDown("Attack 3") || Input.GetMouseButtonDown(2)) {
                //Instantiate(kickPrefab, kickPointTransform);
                animator.SetTrigger("Attack 3");
                currentState = State.Attacking;
            }
            /*else if (Input.GetButtonDown("Kick Left"))
            {
                Instantiate(kickPrefab, kickPointTransform);
                animator.SetTrigger("KickLeft");
            }*/
            else if (Input.GetButtonDown("Special")) {
                if (specialTimer.isDone) {
                    animator.SetTrigger("Special");
                    Invoke("SpawnProjectile", 0.3f);
                    specialTimer.SetTime(specialCooldown);
                    currentState = State.Attacking;
                }
            }
            else if (Input.GetButtonDown("Block")) {
                Debug.Log("Blocking");
                currentState = State.Blocking;
            }
            else if (Input.GetButtonDown("Crouch")) {
                Debug.Log("Crouching");
                currentState = State.Crouching;
            }
        }
        else if (currentState == State.Blocking) {
            if (!Input.GetButton("Block")) {
                currentState = State.Idle;
            }
        }
        else if (currentState == State.Crouching) {
            if (!Input.GetButton("Crouch")) {
                currentState = State.Idle;
            }
        }

        // Animate
        animator.SetBool("Block", currentState == State.Blocking);
        animator.SetBool("Crouch", currentState == State.Crouching);
    }
        
    protected void SpawnProjectile() {
        //problem: this will instantiate a new projectile point each time.
        Instantiate(specialPrefab, SpecialTransform.position, new Quaternion(0,0,0,0));
    }

    public void Idle() {
        currentState = State.Idle;
    }
}

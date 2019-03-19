using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    [SerializeField] float health = 100;

    [SerializeField] GameObject punchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject projectilePrefab;

    public int maxProjectiles;

    Animator animator;
    FPLook fpLook;
    Transform punchPointTransform;
    Transform kickPointTransform;
    Transform projectilePointTransform;
    public GameObject projectilePoint;
    public static int numberOfProjectiles;
    public int cooldown;
    private float timestamp;

    void Awake() {
        animator = GetComponent<Animator>();
        fpLook = GetComponent<FPLook>();
        punchPointTransform = transform.Find("Punch Point");
        kickPointTransform = transform.Find("Kick Point");
        
    }

    void Update() {

        if (Input.GetKeyUp("1"))
        {
            animator.SetBool("Block", false);
        }

        if (Input.GetButtonDown("Punch") || Input.GetMouseButtonDown(0))
        {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("Jab");
        }
        /*else if (Input.GetButtonDown("Punch Left"))
        {
            Instantiate(punchPrefab, punchPointTransform);
            animator.SetTrigger("PunchLeft");
        }*/
        else if (Input.GetMouseButtonDown(1))
        {
            Instantiate(kickPrefab, kickPointTransform);
            animator.SetTrigger("Rising_P");
        }
        /*else if (Input.GetButtonDown("Kick Left"))
        {
            Instantiate(kickPrefab, kickPointTransform);
            animator.SetTrigger("KickLeft");
        }*/
        else if (Input.GetKeyDown("1"))
        {
            animator.SetBool("Block", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (numberOfProjectiles < maxProjectiles) && timestamp < Time.time) 
        {
            timestamp = Time.time + cooldown;
            animator.SetTrigger("Spinkick");
            Invoke("SpawnProjectile", 0.3f);
            Debug.Log(numberOfProjectiles++);
        }
        //Debug.Log(numberOfProjectiles);
    }

    public override void Hurt(float damage) {
        if (!animator.GetBool("Block"))
        {
            health -= damage;
        }
        Debug.Log(health);
        if (health <= 0) Die();
    }

    void Die() {
        Destroy(gameObject);
    }
    public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, projectilePoint.transform.position, new Quaternion(0, 0, 0, 0));
    }
    public void removeProjectile()
    {
        numberOfProjectiles--;
        Debug.Log(numberOfProjectiles);
    }
}

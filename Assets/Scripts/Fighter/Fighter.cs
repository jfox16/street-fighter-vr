using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fighter : Unit
{
    public float health = 1000000;
    public float special = 0;
    public float specialCost = 10;
    public int fighterid;
    public float damageModifier = 1.0f;
    public bool isAlive {get; private set;}
    private Collider mCollider;

    Animator animator;
    FPLook fpLook;
    FighterAnimationHandler animHandler;
    CharacterVO charVO;
    PhotonView photonView;

    Transform punchPointTransform;
    Transform kickPointTransform;
    Transform projectilePointTransform;
    public GameObject projectilePoint;
    public static int numberOfProjectiles;
    public int cooldown;
    private float timestamp;

    /**
     * isMine is true if client is offline, or if client
     * is online and this gameObject belongs to it.
     */
    public bool isMine = false;
    
    //=================================================================================================================

    #region UNITY CALLBACKS

    void Awake() 
    {
        isAlive = true;
        animator    = GetComponent<Animator>();
        fpLook      = GetComponent<FPLook>();
        animHandler = GetComponent<FighterAnimationHandler>();
        photonView  = GetComponent<PhotonView>();
        charVO      = GetComponent<CharacterVO>();

        punchPointTransform = transform.Find("Punch Point");
        kickPointTransform = transform.Find("Kick Point");
        mCollider = this.gameObject.GetComponent<Collider>();
    }

    void Start() {
        if (PhotonNetwork.CurrentRoom == null || photonView.IsMine) {
            isMine = true;
            team = Team.Red;
            animHandler.SetTeam(Team.Red);
        }

        if (isMine && GameControllerDDOL.Instance.CheckVersusScene()) {
            charVO.Intros();
            if (!PhotonNetwork.IsMasterClient) {
                team = Team.Blue;
                animHandler.SetTeam(Team.Blue);
            }
        }
    }

    #endregion
    
    //=================================================================================================================

    #region PUBLIC METHODS

    public override void Hurt(float damage) {
        photonView.RPC("ClientHurt", RpcTarget.All, damage);
    }

    public override void MakeHitParticle(Vector3 hitPosition) {
        photonView.RPC("ClientMakeHitParticle", RpcTarget.All, hitPosition);
    }

    public override void PlayHitSound(Vector3 hitPosition) {
        photonView.RPC("ClientPlayHitSound", RpcTarget.All, hitPosition);
    }

    public float getHealth() {
        return health;
    }
    
    public void ResetFighterHealth()
    {
        health = 100;
        animator.SetTrigger("Idle");
    }

    public void gainSpecial(float val)
    {
        special += val;
        if(special > 100f)
        {
            special = 100f;
        }else if ( special < 0)
        {
            special = 0;
        }
    }
    
    #endregion
    
    //=================================================================================================================

    #region PRIVATE METHODS

    void Die() {
        photonView.RPC("ClientDie", RpcTarget.All);
    }

    void Respawn() {
        NetworkController.LeaveRoom();
    }

    #endregion
    
    //=================================================================================================================

    #region PUN RPCS

    [PunRPC]
    public void ClientHurt(float damage) {
        Debug.Log(gameObject.name + "is hurt for " + damage + " damage!");
        if (animator.GetBool("Block")) {
            health -= damage * 0.5f;
        }
        else {
            animator.SetTrigger("Hurt");
            health -= damage;
        }
        if (health <= 0) {
            health = 0;
            Die();
        }
    }

    [PunRPC]
    public void ClientDie() 
    {
        if (isAlive) {
            isAlive = false;
            animator.SetTrigger("Die");
            mCollider.enabled = !mCollider.enabled;
            if (isMine) {
                Invoke("Respawn", 5);
            }
        }
    }

    [PunRPC]
    public void ClientMakeHitParticle(Vector3 hitPosition) {
        GameObject _hitSpark = (GameObject)Resources.Load("Particles/HitSpark");
        Instantiate(_hitSpark, hitPosition, transform.rotation);
    }

    [PunRPC]
    public void ClientPlayHitSound(Vector3 hitPosition) {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Fighting/Punch", hitPosition);
    }

    #endregion
    
    //=================================================================================================================
}

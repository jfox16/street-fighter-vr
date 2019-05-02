using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    private PhotonView photon;
    public int value;
    public GameObject myCharacter;

    // Start is called before the first frame update
    void Start()
    {
        photon = GetComponent<PhotonView>();

        if(photon.IsMine)
        {
            photon.RPC("RPC_AddCharacrter", RpcTarget.AllBuffered, PlayerInfo.PI.selectedCharacter);
        }
    }

    [PunRPC]
    void RPC_AddCharacrter(int whichCharacter)
    {
        value = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], transform.position, transform.rotation, transform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

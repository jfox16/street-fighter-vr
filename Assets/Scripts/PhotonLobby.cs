using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : GameController
{
    // Start is called before the first frame update

    [SerializeField] public static PhotonLobby lobby;

    [SerializeField] public GameObject button;
    [SerializeField] public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    void Start()
    {
       PhotonNetwork.ConnectUsingSettings();   
    }

    public override void OnConnectedToMaster()
    {
        print("Player has been connected to photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public void OnButtonClick()
    {
        CreateRoom();

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to Join Random Room but failed.");
        CreateRoom();
       
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOPs = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Test Room" + 99999, roomOPs);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to creat Random Room but failed.");
        CreateRoom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickCharacterPick(int whichCharacter)
    {
        if (PlayerInfo.PI != null)
        {
            PlayerInfo.PI.selectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("My Character", whichCharacter);
        }
    }
    public void OnCancelButtonClick()
    {
        cancelButton.SetActive(false);
        button.SetActive(true);
        PhotonNetwork.LeaveRoom();  // leaves room
    }
}

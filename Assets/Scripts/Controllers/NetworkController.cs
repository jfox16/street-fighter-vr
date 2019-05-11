using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController Instance = null;

    string startRoomPath = "VR Start";
    string multiplayerRoomPath = "Scenes/Multiplayer/MultiplayerRoom";
    
    //=================================================================================================================

    #region UNITY CALLBACKS

    void Awake() 
    {
        if (Instance == null) {
            // Set this as Instance and keep it from being destroyed across scenes.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            // If an instance of this already exists, destroy this one.
            Destroy(gameObject);
            return;
        }

        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        PhotonNetwork.NetworkingClient.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
    }

    #endregion

    //=================================================================================================================

    #region PHOTON CALLBACKS

    override public void OnConnectedToMaster() 
    {
        Debug.Log("NetworkController: Connected to " + PhotonNetwork.NetworkingClient.Server + "!");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(1);
    }

    override public void OnDisconnected(DisconnectCause cause) 
    {
        Debug.Log("NetworkController: Disconnected from Server. " + cause);
        SceneManager.LoadScene(startRoomPath);
    }

    override public void OnJoinedRoom() {
        Debug.Log("NetworkController: Joined " + PhotonNetwork.CurrentRoom.Name + "!");
        PhotonNetwork.LoadLevel(2);
    }

    #endregion
    
    //=================================================================================================================

    #region PUBLIC METHODS

    public static void ConnectToServer() 
    {
        Debug.Log("NetworkController: Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public static void DisconnectFromServer() 
    {
        Debug.Log("NetworkController: Disconnecting...");
        PhotonNetwork.Disconnect();
    }

    public static void JoinRoom(string roomName) 
    {
        Debug.Log("NetworkController: Joining " + roomName + "...");

        RoomOptions roomOPs = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        PhotonNetwork.JoinOrCreateRoom(
            roomName,
            roomOPs,
            Photon.Realtime.TypedLobby.Default
        );
    }

    #endregion
    
    //=================================================================================================================
}

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
    int newRoomSceneIndex = 0;
    
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
        // Switch Scene to Multiplayer Lobby
        PhotonNetwork.LoadLevel(2);
    }

    override public void OnDisconnected(DisconnectCause cause) 
    {
        Debug.Log("NetworkController: Disconnected from Server. " + cause);
        SceneManager.LoadScene(startRoomPath);
    }

    override public void OnCreatedRoom()
    {
        Debug.Log("NetworkController: Created " + PhotonNetwork.CurrentRoom.Name + "!");
        PhotonNetwork.LoadLevel(newRoomSceneIndex);
    }

    override public void OnJoinedRoom() 
    {
        Debug.Log("NetworkController: Joined " + PhotonNetwork.CurrentRoom.Name + "!");
    }

    override public void OnLeftRoom() 
    {
        Debug.Log("NetworkController: Returning to lobby.");
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
        if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer) return;
        Debug.Log("NetworkController: Disconnecting...");
        PhotonNetwork.Disconnect();
    }

    public static void JoinRoom(string roomName, int sceneIndex) 
    {
        if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer) return;

        Debug.Log("NetworkController: Joining " + roomName + "...");

        // If a new room is created, newRoomSceneIndex will determine which scene it uses.
        Instance.newRoomSceneIndex = sceneIndex;

        // These RoomOptions limit the amount of players per room to 2.
        RoomOptions roomOPs = new RoomOptions() {
            IsVisible  = true,
            IsOpen     = true,
            MaxPlayers = 2
        };
        // Tries to Join a room named roomName. If it is not found, creates one instead.
        PhotonNetwork.JoinOrCreateRoom(
            roomName,
            roomOPs,
            Photon.Realtime.TypedLobby.Default
        );
    }

    public static void LeaveRoom()
    {
        if (PhotonNetwork.NetworkingClient.Server != ServerConnection.GameServer) return;
        Debug.Log(PhotonNetwork.NetworkingClient.Server);
        Debug.Log("NetworkController: Leaving room.");
        PhotonNetwork.LeaveRoom();
    }

    #endregion
    
    //=================================================================================================================
}

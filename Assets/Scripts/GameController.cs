using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 playerSpawnPosition = new Vector3(0, 0, -10);

    [SerializeField] Text statusText;

    [SerializeField] GameObject gameStartUi;
    [SerializeField] InputField roomNameField;
    [SerializeField] Toggle onlineToggle;
    [SerializeField] Dropdown fighterDropdown;

    GameObject blueGuy;
    GameObject unityChan;
    GameObject mechaMan;
    GameObject networkBlueGuy;
    GameObject networkUnityChan;
    GameObject networkMechaMan;

    [SerializeField] GameObject sparkles;

    GameObject player = null;



    #region UNITY CALLBACKS

    void Awake() {
        // Pre-load fighter prefabs from Resources
        blueGuy = (GameObject)Resources.Load("Fighters/Blue Guy");
        unityChan = (GameObject)Resources.Load("Fighters/Unity-chan");
        mechaMan = (GameObject)Resources.Load("Fighters/Mecha Man");
        networkBlueGuy = (GameObject)Resources.Load("Network Fighters/Network Blue Guy");
        // networkUnityChan = (GameObject)Resources.Load("Network Fighters/Network Unity-chan");
        // networkRoboMan = (GameObject)Resources.Load("Network Fighters/Network Mecha Man");

        // Adds this class as a callback target for PUN Networking
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        // Sets the app's Id for PUN Networking
        PhotonNetwork.NetworkingClient.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
    }

    void Update() {
        // Unlock cursor when Cancel(Esc) is pressed
        if (Input.GetButtonDown("Cancel")) {
            Cursor.lockState = CursorLockMode.None;
        }
        // Lock cursor when Mouse clicked
        if (Input.GetMouseButtonDown(0)) {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    #endregion



    #region PUN CALLBACKS

    override public void OnConnectedToMaster() {
        Debug.Log("Connected to server! Joining or creating room " + roomNameField.text + "...");
        statusText.text = "Connected to server! Joining or creating room " + roomNameField.text + "...";

        /* Joins a room named roomName, or creates one if it doesn't exist. */
        PhotonNetwork.JoinOrCreateRoom(
            roomNameField.text, 
            new Photon.Realtime.RoomOptions(),
            Photon.Realtime.TypedLobby.Default
        );
    }

    override public void OnJoinedRoom() {
        Debug.Log("Connected to room! Spawning player...");
        statusText.text = "Connected to room! Spawning player...";
        SpawnNetworkPlayer();
    }

    #endregion


    
    void ConnectToServer() {
        Debug.Log("Connecting to server...");
        statusText.text = "Connecting to server...";
        // Connects to server, choosing the one with lowest ping
        PhotonNetwork.ConnectToBestCloudServer();
    }

    void SpawnPlayer() {
        GameObject playerPrefab = null;
        // Determine which fighter to spawn based on dropdown
        if (fighterDropdown.value == 0) {
            playerPrefab = blueGuy;
        }
        else if (fighterDropdown.value == 1) {
            playerPrefab = unityChan;
        }
        else if (fighterDropdown.value == 2) {
            playerPrefab = mechaMan;
        }

        // Spawn a player at playerSpawnPosition
        player = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        Debug.Log("Player spawned!");
        statusText.text = "Player spawned!";
        // Attach main camera to player as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FighterLook>().AttachCamera(mainCamera);

        // Sparkles!!
        Instantiate(sparkles, player.transform.position, Quaternion.identity);
    }

    void SpawnNetworkPlayer() {
        string playerPrefabName = null;
        // Determine which fighter to spawn based on dropdown
        if (fighterDropdown.value == 0) {
            playerPrefabName = "Fighters/Network Blue Guy";
        }
        else if (fighterDropdown.value == 1) {
            playerPrefabName = "Fighters/Network Unity-chan";
        }
        else if (fighterDropdown.value == 2) {
            playerPrefabName = "Fighters/Network Mecha Man";
        }

        /* When instantiating an object on the network, use PhotonNetwork.Instantiate instead.
        This will properly initialize it on the network. Also, instead of using direct prefab
        reference, this method takes the name of the prefab as a string, and the prefab will 
        be retrieved from the Assets/Resources folder. */
        player = PhotonNetwork.Instantiate(playerPrefabName, playerSpawnPosition, Quaternion.identity);
        Debug.Log("Player spawned!");
        statusText.text = "Player spawned!";
        // Attach main camera to player's as first person view
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<FighterLook>().AttachCamera(mainCamera);

        // Sparkles!!
        Instantiate(sparkles, player.transform.position, Quaternion.identity);
    }



    #region PUBLIC METHODS

    public void GameStart() {
        if (onlineToggle.isOn) {
            ConnectToServer();
        }
        else {
            SpawnPlayer();
        }

        gameStartUi.SetActive(false);
    }

    public GameObject GetPlayer() {
        return player;
    }

    #endregion


}

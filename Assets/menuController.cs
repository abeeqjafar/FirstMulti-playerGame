using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    [SerializeField] private string versionName = "0.1";

    [SerializeField] private GameObject usernameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private InputField usernameInput;

    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject StartButton;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }
    // Start is called before the first frame update
    private void Start()
    {
        usernameMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeUsernameInput()
    {
        if(usernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void setUsername()
    {
        usernameMenu.SetActive(false); 
        PhotonNetwork.playerName = usernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(createGameInput.text, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void joinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(joinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
}

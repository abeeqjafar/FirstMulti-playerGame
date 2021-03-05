using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;
    public GameObject disconnectUI;
    public Text pingText;
    private bool off;
    public GameObject playerFeed;
    public GameObject feedGrid;

    private void Awake()
    {
        gameCanvas.SetActive(true);
    }

    private void Update()
    {
        checkInput();    
        pingText.text = "Ping: " + PhotonNetwork.GetPing();
    }

    private void checkInput()
    {
        if(off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            off = false;
        }
        else if (!off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            off = true;
        }
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(this.transform.position.x*randomValue, this.transform.position.y), Quaternion.identity, 0);
        gameCanvas.SetActive(false);
        sceneCamera.SetActive(false);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    private void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(playerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(feedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " has joined the game";
        obj.GetComponent<Text>().color = Color.green;

    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(playerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(feedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " has left the game";
        obj.GetComponent<Text>().color = Color.red;

    }
}

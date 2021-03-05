using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class chatManager : MonoBehaviour
{
    public Player p1Move;
    public PhotonView photonView;
    public GameObject bubbleSpeechObject;
    public Text updatedText;

    private InputField ChatInputField;
    private bool disableSend;

    private void Awake()
    {
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if(!disableSend && ChatInputField.isFocused)
            {
                if(ChatInputField.text!="" && ChatInputField.text.Length>0 && Input.GetKeyDown(KeyCode.Slash))
                {
                    photonView.RPC("SendMessage", PhotonTargets.AllBuffered, ChatInputField.text);
                    bubbleSpeechObject.SetActive(true);
                    ChatInputField.text = "";
                    disableSend = true;
                }
            }
        }
    }

    [PunRPC]
    private void SendMessage(string message)
    {
        updatedText.text = message;
        StartCoroutine("Remove");
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        bubbleSpeechObject.SetActive(false);
        disableSend = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo photonMessageInfo)
    {
        if (stream.isWriting)
        {
            stream.SendNext(bubbleSpeechObject.active);
        }
        else if (stream.isReading)
        {
            bubbleSpeechObject.SetActive((bool)stream.ReceiveNext());
        }
    }
}

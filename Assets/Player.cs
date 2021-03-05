using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{
    public Rigidbody2D rb;
    //public Animator anim;
    public GameObject playerCamera;
    public PhotonView PhotonView;
    public SpriteRenderer sr;
    public Text playerNameText;

    public float moveSpeed;
    public float jumpForce;

    private void Awake()
    {
        if (photonView.isMine)
        {
            playerCamera.SetActive(true);
            playerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            playerNameText.text = photonView.owner.name;
            playerNameText.color = Color.cyan;
        }
    }

    private void checkInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.isMine)
        {
            checkInput();
        }

    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }
    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }
}

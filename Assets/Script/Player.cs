using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    public PhotonView photonView;
    public Rigidbody2D rb;
    //public Animator animator;
    public GameObject PlayerCamera;
    public SpriteRenderer Sr;
    public SpriteRenderer Weapon;
    public Text PlayerName;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;
    private int JumpTimes;
    private int AllowedTime = 2;
    // Start is called before the first frame update
    private void Awake()
    {
        JumpTimes = AllowedTime;
        if (photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
            PlayerName.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerName.text = photonView.Owner.NickName;
            PlayerName.color = Color.cyan;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.IsMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        var Move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += Move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            photonView.RPC("FlipTrue", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            photonView.RPC("FlipFalse", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Jump", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void FlipTrue()
    {
        Sr.flipX = true;
        Weapon.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        Sr.flipX = false;
        Weapon.flipX = false;
    }

    [PunRPC]
    private void Jump()
    {
        if (JumpTimes > 0)
        {
            rb.AddForce(transform.up * JumpForce);
            IsGrounded = false;
            JumpTimes -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
            JumpTimes = AllowedTime;
        }
    }
}

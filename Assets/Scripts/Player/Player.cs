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
    public Animator animator;
    public GameObject PlayerCamera;

    public BoxCollider2D Collider;
    public BoxCollider2D SlideCollider;
    public BoxCollider2D CrouchCollider;

    public SpriteRenderer Sr;
    public Text PlayerName;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;
    private int JumpTimes;
    private int AllowedTime = 2;

    public float slideSpeed = 5f;
    private bool slide = false;
    private bool crouch = false;

    public GameObject ProjectileObjectRight;
    public GameObject ProjectileObjectLeft;

    public Transform FirePosRight;
    public Transform FirePosLeft;

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
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.IsMine)
        {
            this.gameObject.tag = "Player";
            CheckInput();
        }
        else
        {
            this.gameObject.tag = "Enemy";
        }
    }

    private void CheckInput()
    {
        var Move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += Move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            photonView.RPC("FlipTrue", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            photonView.RPC("FlipFalse", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            photonView.RPC("Jump", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Shooting();
            photonView.RPC("AnimatedSprite", RpcTarget.AllBuffered, "Attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) 
        {
            photonView.RPC("Slide", RpcTarget.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            photonView.RPC("Crouch", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void AnimatedSprite(string state)
    {
        animator.Play(state);
    }

    private void Shooting()
    {
        if (Sr.flipX == false)
        {
            GameObject obj = PhotonNetwork.Instantiate(ProjectileObjectRight.name, new Vector2(FirePosRight.transform.position.x, FirePosRight.transform.position.y), Quaternion.identity, 0);
        }
        if (Sr.flipX == true)
        {
            GameObject obj = PhotonNetwork.Instantiate(ProjectileObjectLeft.name, new Vector2(FirePosLeft.transform.position.x, FirePosLeft.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChangeDir", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void FlipTrue()
    {
        Sr.flipX = true;
        photonView.RPC("AnimatedSprite", RpcTarget.AllBuffered, "Run");
    }

    [PunRPC]
    private void FlipFalse()
    {
        Sr.flipX = false;
        photonView.RPC("AnimatedSprite", RpcTarget.AllBuffered, "Run");
    }

    [PunRPC]
    private void Jump()
    {
        if (JumpTimes > 0)
        {
            photonView.RPC("AnimatedSprite", RpcTarget.AllBuffered, "Jump");
            rb.AddForce(transform.up * JumpForce);
            IsGrounded = false;
            JumpTimes -= 1;
        }
    }

    [PunRPC]
    private void Slide()
    {
        slide = true;
        Collider.enabled = false;
        SlideCollider.enabled = true;
        CrouchCollider.enabled = false;

        photonView.RPC("AnimatedSprite", RpcTarget.AllBuffered, "Sliding");
        if (Sr.flipX == false)
        {
            rb.AddForce(Vector2.right * slideSpeed);
        }
        else if (Sr.flipX == true)
        {
            rb.AddForce(Vector2.left * slideSpeed);
        }
        StartCoroutine("StopSlide");
    }

    [PunRPC]
    private void Crouch()
    {
        if (slide == false)
        {
            Collider.enabled = false;
            CrouchCollider.enabled = true;
            crouch = true;
        }
        StartCoroutine("StopCrouch");
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(1f);
        Collider.enabled = true;
        SlideCollider.enabled = false;
        CrouchCollider.enabled = false;
        slide = false;
    }

    IEnumerator StopCrouch()
    {
        yield return new WaitForSeconds(0.6f);
        Collider.enabled = true;
        SlideCollider.enabled = false;
        CrouchCollider.enabled = false;
        crouch = false;
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

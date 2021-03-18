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
    public Text PlayerName;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;
    // Start is called before the first frame update
    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
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
            Sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Sr.flipX = false;
        }
    }
}

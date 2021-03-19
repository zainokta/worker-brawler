using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CharSelectControl : MonoBehaviour
{
    [SerializeField] CharPick charPick;
    [SerializeField] Animator animator;
    //[SerializeField] AnimatorFunction animatorFunction;
    [SerializeField] public int thisIndex;
    public static bool notShow = false;
    public static bool GoToCredit = false;

    //Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (charPick.index == thisIndex)
        {
            animator.SetBool("Select", true);

            if (Input.GetAxis("Submit") == 1)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                gm.PlayerChoosen = thisIndex;
                SceneManager.LoadScene("Lobby");
            }
            /*if (animator.GetBool("Pressed"))
            {
                animator.SetBool("Pressed", false);
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
                {
                    if (charPick.index == 0)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                    }
                    else if (charPick.index == 1)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                    }
                    else if (charPick.index == 2)
                    {
                        Application.Quit();
                    }
                }

                else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuMap")
                {
                    if (charPick.index == 0)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                    }
                }
            }*/
        }
        else
        {
            animator.SetBool("Select", false);
        }
    }
}

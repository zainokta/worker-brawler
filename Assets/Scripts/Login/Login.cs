using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Login : MonoBehaviour
{
    [Header("UI Panel")]
    [SerializeField] GameObject UserNameMenu;
    [SerializeField] GameObject HelloMenu;
    [SerializeField] GameObject FalseMenu;

    [Header("Username")]
    [SerializeField] private InputField UsernameInput;

    [Header("Text")]
    [SerializeField] Text OpenHello;

    GameManager gameManager;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        UserNameMenu.SetActive(true);
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void SetUsername()
    {
        soundManager.Play("Click");
        if(UsernameInput.text.Length > 2)
        {
            UserNameMenu.SetActive(false);
            PhotonNetwork.NickName = UsernameInput.text;
            OpenFuncHello();
        }
        if(UsernameInput.text.Length <= 2)
        {
            FalseMenu.SetActive(true);
            StartCoroutine(FalseTextUI());
        }
    }
    void OpenFuncHello()
    {
        FalseMenu.SetActive(false);
        HelloMenu.SetActive(true);
        OpenHello.text = PhotonNetwork.NickName;
        soundManager.Play("Welcome");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        gameManager.FadeScreen("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FalseTextUI()
    {
        yield return new WaitForSeconds(3);
        FalseMenu.SetActive(false);
    }
}

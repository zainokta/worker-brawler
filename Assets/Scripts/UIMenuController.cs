using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{


    GameManager gameManager;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        gameManager.FadeScreen("FadeIn");
        soundManager.Play("MainTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

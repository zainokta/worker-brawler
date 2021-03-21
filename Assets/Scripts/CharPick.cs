using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharPick : MonoBehaviour
{
    SoundManager soundManager;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharSelect(int thisIndex)
    {
        soundManager.Play("Click");
        GameManager gm = FindObjectOfType<GameManager>();
        gm.pl.PlayerChoosen = thisIndex;
        SceneManager.LoadScene("Lobby");
    }
}

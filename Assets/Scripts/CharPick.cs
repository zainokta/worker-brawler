using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharPick : MonoBehaviour
{
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharSelect(int thisIndex)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.pl.PlayerChoosen = thisIndex;
        SceneManager.LoadScene("Lobby");
    }
}

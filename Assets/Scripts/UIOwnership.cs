using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOwnership : MonoBehaviour
{
    public GameObject panel;

    public void SetActivePanel(bool a)
    {
        UIMenuController uIMenu = FindObjectOfType<UIMenuController>();
        uIMenu.SetPanel(panel, a);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransessionToKeybinds : MonoBehaviour
{
    [SerializeField] GameObject menuOptions;
    [SerializeField] GameObject menuBack;
    [SerializeField] GameObject keybindsPannel;

    public void GoToKeybinds()
    {
        menuOptions.SetActive(false);
        menuBack.SetActive(false);
        keybindsPannel.SetActive(true);
    }

    public void BackToMenu()
    {
        keybindsPannel.SetActive(false);
        menuBack.SetActive(true);
        menuOptions.SetActive(true);
    }
}

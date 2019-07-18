using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MenuState { Main, NewGame, Continue, Options }

public class MenuManager : MonoBehaviour
{
    MenuState state;
    public Animator NGanim;
    public Animator Canim;
    public Animator Oanim;

    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.Main;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MenuState.Main:
                NGanim.SetTrigger("Main Menu");
                Canim.SetTrigger("Main Menu");
                Oanim.SetTrigger("Main Menu");
                break;
            case MenuState.NewGame:
                NGanim.SetTrigger("This Option");
                Canim.SetTrigger("Other Option");
                Oanim.SetTrigger("Other Option");
                break;
            case MenuState.Continue:
                NGanim.SetTrigger("Other Option");
                Canim.SetTrigger("This Option");
                Oanim.SetTrigger("Other Option");
                break;
            case MenuState.Options:
                NGanim.SetTrigger("Other Option");
                Canim.SetTrigger("Other Option");
                Oanim.SetTrigger("This Option");
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Main();
    }

    public void NewGame()
    {
        state = MenuState.NewGame;
    }

    public void Continue()
    {
        state = MenuState.Continue;
    }

    public void Options()
    {
        state = MenuState.Options;
    }

    public void Main()
    {
        state = MenuState.Main;
    }
}

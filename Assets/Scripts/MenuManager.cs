using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MenuState { Main, NewGame, Continue, Options }

public class MenuManager : MonoBehaviour
{
    MenuState state;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.Main;
        anim.SetBool("Main", true);
        anim.SetBool("NewGame", false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MenuState.Main:
                anim.SetBool("NewGame", false);
                anim.SetBool("Main", true);
                break;
            case MenuState.NewGame:
                anim.SetBool("NewGame", true);
                anim.SetBool("Main", false);
                break;
            case MenuState.Continue:
                anim.SetBool("NewGame", false);
                anim.SetBool("Main", false);
                break;
            case MenuState.Options:
                anim.SetBool("NewGame", false);
                anim.SetBool("Main", false);
                break;
            default:
                break;
        }
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

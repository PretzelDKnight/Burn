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

    public AudioSource SFX;

    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.Main;
        NGanim.SetBool("This Option", false);
        Canim.SetBool("This Option", false);
        Oanim.SetBool("This Option", false);

        NGanim.SetBool("Other Option", false);
        Canim.SetBool("Other Option", false);
        Oanim.SetBool("Other Option", false);

        SFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MenuState.Main:
                NGanim.SetBool("This Option", false);
                Canim.SetBool("This Option", false);
                Oanim.SetBool("This Option", false);
                Invoke("Reset", 0.5f);
                break;
            case MenuState.NewGame:
                NGanim.SetBool("This Option", true);
                Canim.SetBool("Other Option", true);
                Oanim.SetBool("Other Option", true);
                break;
            case MenuState.Continue:
                NGanim.SetBool("Other Option", true);
                Canim.SetBool("This Option", true);
                Oanim.SetBool("Other Option", true);
                break;
            case MenuState.Options:
                NGanim.SetBool("Other Option", true);
                Canim.SetBool("Other Option", true);
                Oanim.SetBool("This Option", true);
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
        SFX.Play();
    }

    public void Continue()
    {
        state = MenuState.Continue;
        SFX.Play();
    }

    public void Options()
    {
        state = MenuState.Options;
        SFX.Play();
    }

    public void Main()
    {
        if (state != MenuState.Main)
            SFX.Play();
        state = MenuState.Main;
        //Reset();
    }

    private void Reset()
    {
        NGanim.SetBool("Other Option", false);
        Canim.SetBool("Other Option", false);
        Oanim.SetBool("Other Option", false);
    }
}

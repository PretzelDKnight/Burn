using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    float i;
    public delegate void PrintMaster9000(float b);
    public static event PrintMaster9000 HahaVeryFunny;
    public delegate void PlayerControls();
    public static event PlayerControls LeftAndRight;
    public static event PlayerControls Jumping;
    public static event PlayerControls Interacting;
    public static event PlayerControls Attacking;
    public static event PlayerControls Dash;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            i++;
            if (HahaVeryFunny != null)
                HahaVeryFunny(i);
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if (LeftAndRight != null)
                LeftAndRight();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Jumping != null)
                Jumping();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (Interacting != null)
                Interacting();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (Attacking != null)
                Attacking();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (Dash != null)
                Dash();
        }
    }
}

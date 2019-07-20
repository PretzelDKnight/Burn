﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    float i;
    public delegate void PrintMaster9000(float b);
    public static event PrintMaster9000 HahaVeryFunny;
    public delegate void Movement();
    public static event Movement LeftAndRight;
    public static event Movement JumpingForward;
    public static event Movement JumpingBack;
    public static event Movement Jumping;
    public static event Movement Ducking;

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
        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetAxisRaw("Horizontal") > 0)
        {
            if (JumpingForward != null)
                JumpingForward();
        }
         else if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetAxisRaw("Horizontal") < 0)
        {
            if (JumpingBack != null)
                JumpingBack();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Jumping != null)
                Jumping();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Ducking != null)
                Ducking();
        }
    }
}
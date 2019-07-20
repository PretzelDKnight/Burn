﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] string LayermaskName;
    bool grounder = true;
    bool inAirRight = false;
    bool inAirLeft = false;
    float halfHightVertical;



    void Start()
    {
        InputManager.HahaVeryFunny += KyleHasDirtyUnderwear;
        InputManager.LeftAndRight += MoveForawrdOrBack;
        InputManager.JumpingForward += JumpForward;
        InputManager.JumpingBack += JumpBack;
        InputManager.Jumping += JumpStationary;
        InputManager.Ducking += Duck;
        halfHightVertical = transform.localScale.y / 2;
    }


    void KyleHasDirtyUnderwear(float i)
    {
        Debug.Log("KYLE Y U DO DIS??!!" + " Kyle has spent " + i + " days in the same stinky underwear");
    }
    void MoveForawrdOrBack()
    {
        RayCastingFunction(Vector3.down);
        
        if (inAirRight && Input.GetAxisRaw("Horizontal") > 0)
            transform.position += new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime;
        else if (inAirLeft && Input.GetAxisRaw("Horizontal") < 0)
            transform.position += new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime;
        else
        {
            inAirRight = false;
            inAirLeft = false;
        }
        if (grounder)
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;

    }
    void JumpForward()
    {
        RayCastingFunction(Vector3.down);
        if (grounder)
        {
            playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            inAirRight = true;
            inAirLeft = false;
            Debug.Log("<color=green> right </color>");
        }
    }

    void JumpBack()
    {
        RayCastingFunction(Vector3.down);
        if (grounder)
        {
            playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            inAirRight = false;
            inAirLeft = true;
            Debug.Log("<color=yellow> left </color>");
        }
    }

    void JumpStationary()
    {
        RayCastingFunction(Vector3.down);
        if (grounder)
        {
            playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            inAirLeft = false;
            inAirRight = false;
        }
    }


    void Duck()
    {
        print("<color=red> DUCKING NOW </color>");
    }

    void RayCastingFunction(Vector3 rayDirection)
    {
        if(Physics.Raycast(transform.position, rayDirection, halfHightVertical, LayerMask.GetMask(LayermaskName)))
        {
            grounder = true;
        }
        else
        {
            grounder = false;
        }
    }
}

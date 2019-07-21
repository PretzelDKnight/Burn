using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] string LayermaskName;
    bool grounder = true;
    bool inAirRight = false;
    bool inAirLeft = false;
    bool onGround = true;
    float halfHightVertical;



    void Start()
    {
        InputManager.HahaVeryFunny += KyleHasDirtyUnderwear;
        InputManager.LeftAndRight += MoveForawrdOrBack;
        InputManager.JumpingForward += JumpForward;
        InputManager.JumpingBack += JumpBack;
        InputManager.Jumping += JumpStationary;
        InputManager.Ducking += Duck;
        InputManager.Ineracting += Interact;
        InputManager.Attacking += Attack;
        InputManager.Special += SpecialAttack;
        halfHightVertical = transform.localScale.y / 2;
    }


    void KyleHasDirtyUnderwear(float i)
    {
        Debug.Log("KYLE Y U DO DIS??!!" + " Kyle has spent " + i + " days in the same stinky underwear");
    }
    void MoveForawrdOrBack()
    {
        if (onGround)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;
        }
        else if (inAirRight && Input.GetAxisRaw("Horizontal") > 0)
            transform.position += new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime;
        else if (inAirLeft && Input.GetAxisRaw("Horizontal") < 0)
            transform.position += new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime;
        else
        {
            inAirRight = false;
            inAirLeft = false;
        }
    }
    void JumpForward()
    {
        RayCastingFunction(Vector3.down);
        if (grounder)
        {
            playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            inAirRight = true;
            inAirLeft = false;
            Debug.Log("<color=green> right </color>" + onGround);
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
            Debug.Log("<color=yellow> left </color>" + onGround);
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

    void Interact()
    {
        print("<color=pink> INTERACTING NOW</color>");
    }

    void Attack()
    {
        print("<color=cyan> ATTACKING NOW </color>");
    }

    void SpecialAttack()
    {
        print("<color=cyan> USING SPECIAL-ATTACK NOW </color>");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = false;
    }

}

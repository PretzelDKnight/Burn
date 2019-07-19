using System.Collections;
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
        InputManger.HahaVeryFunny += KyleHasDirtyUnderwear;
        InputManger.LeftAndRight += MoveForawrdOrBack;
        InputManger.Jumping += Jump;
        InputManger.Ducking += Duck;
        halfHightVertical = transform.localScale.y / 2;
    }


    void KyleHasDirtyUnderwear(float i)
    {
        Debug.Log("KYLE Y U DO DIS??!!" + " Kyle has spent " + i + " days in the same stinky underwear");
    }
    void MoveForawrdOrBack()
    {
        RayCastingFunction(Vector3.down);
        if (grounder)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;
            inAirRight = false;
            inAirLeft = false;
        }
        else if (inAirRight && Input.GetAxisRaw("Horizontal") > 0)
            transform.position += new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime;
        else if (inAirLeft && Input.GetAxisRaw("Horizontal") < 0)
            transform.position += new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime;

    }
    void Jump()
    {
        RayCastingFunction(Vector3.down);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (grounder)
                playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            inAirRight = true;
            inAirLeft = false;
            Debug.Log("<color=green> right </color>");
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (grounder)
                playerRb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            inAirRight = false;
            inAirLeft = true;
            Debug.Log("<color=yellow> left </color>");
        }
        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            if (grounder)
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

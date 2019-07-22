using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] LayerMask layerMask;
    bool grounder = true;
    float halfHightVertical;



    void Start()
    {
        InputManager.HahaVeryFunny += KyleHasDirtyUnderwear;
        InputManager.LeftAndRight += MoveForawrdOrBack;
        InputManager.Jumping += Jump;
        InputManager.Ducking += Duck;
        InputManager.Ineracting += Interact;
        InputManager.Attacking += Attack;
        InputManager.Special += SpecialAttack;
        halfHightVertical = transform.localScale.y / 2+.1f;

        playerRb = GetComponent<Rigidbody2D>();
    }


    void KyleHasDirtyUnderwear(float i)
    {
        Debug.Log("KYLE Y U DO DIS??!!" + " Kyle has spent " + i + " days in the same stinky underwear");
    }
    void MoveForawrdOrBack()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;
    }

    void Jump()
    {
        RayCastingFunction(Vector3.down);
        if (grounder)
        {
            playerRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
        var Check = Physics2D.Raycast(transform.position, rayDirection, halfHightVertical, layerMask);
        Debug.DrawLine(transform.position, transform.position + rayDirection* halfHightVertical);
        Debug.Log(Check.collider); 
        if (Check.collider)
        {
            grounder = true;
        }
        else
        {
            grounder = false;
        }
    }

}

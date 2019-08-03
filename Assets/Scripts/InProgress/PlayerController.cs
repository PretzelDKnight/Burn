using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpForceForward;
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] LayerMask wallLayerMask;
    Rigidbody2D playerRb;
    int jumpNumber = 2;
    bool grounded = true;
    bool onLeftWall = false;
    bool onRightWall = false;
    bool notInAir = false;
    bool isDashing = false;
    float halfHightVertical;
    float halfHightHorizontal;



    void Start()
    {
        InputManager.HahaVeryFunny += KyleHasDirtyUnderwear;
        InputManager.LeftAndRight += MoveForawrdOrBack;
        InputManager.Jumping += Jump;
        InputManager.Ducking += Duck;
        InputManager.Interacting += Interact;
        InputManager.Attacking += Attack;
        InputManager.Special += SpecialAttack;
        InputManager.Dash += Dashing;
        halfHightVertical = transform.localScale.y / 2 +.1f;
        halfHightHorizontal = transform.localScale.x / 2 + .1f;

        playerRb = GetComponent<Rigidbody2D>();
    }


    void KyleHasDirtyUnderwear(float i)
    {
        Debug.Log("KYLE Y U DO DIS??!!" + " Kyle has spent " + i + " days in the same stinky underwear");
    }
    void MoveForawrdOrBack()
    {
        RayCastingFunction();
        if (!isDashing)
        {
            if (grounded)
                notInAir = true;

            if ((onLeftWall || onRightWall) && !grounded) ;

            else
                playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, playerRb.velocity.y);
        }
    }

    void Jump()
    {
        RayCastingFunction();
        if (grounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, Input.GetAxisRaw("Jump") * jumpForce);
            jumpNumber = 1;
            notInAir = false;
        }
        else if(onLeftWall || onRightWall)
        {
            if(onRightWall)
            {
                playerRb.velocity = new Vector2(0, 0);
                playerRb.AddForce(new Vector2(-jumpForceForward, jumpForce), ForceMode2D.Impulse);
            }
            else if (onLeftWall)
            {
                playerRb.velocity = new Vector2(0, 0);
                playerRb.AddForce(new Vector2(jumpForceForward, jumpForce), ForceMode2D.Impulse);
            }
            notInAir = false;
        }
        else if(jumpNumber > 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.velocity = new Vector2(playerRb.velocity.x, Input.GetAxisRaw("Jump") * jumpForce);
            jumpNumber--;
        }
    }

    void Dashing()
    {
        Physics2D.IgnoreLayerCollision(11, 12, ignore: true);
        isDashing = true;
        StartCoroutine(StopDash(dashDuration));
        playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dashSpeed, 0);
        print("<color=yellow> DASHING </color>");
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


    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") == 0 && notInAir)
        {
            playerRb.velocity = new Vector2(0,playerRb.velocity.y);
        }
        
    }


    void RayCastingFunction()
    {
        var checkDown = Physics2D.Raycast(transform.position, Vector3.down, halfHightVertical, floorLayerMask);
        Debug.DrawLine(transform.position, Vector3.down * halfHightVertical, Color.red);
        if (checkDown.collider)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        var checkLeft = Physics2D.Raycast(transform.position, Vector3.left, halfHightHorizontal, wallLayerMask);
        Debug.DrawLine(transform.position, Vector3.left * halfHightHorizontal, Color.yellow);
        if (checkLeft.collider)
        {
            onLeftWall = true;
        }
        else
        {
            onLeftWall = false;
        }

        var checkRight = Physics2D.Raycast(transform.position, Vector3.right, halfHightHorizontal, wallLayerMask);
        Debug.DrawLine(transform.position, Vector3.right * halfHightHorizontal, Color.green);
        if (checkRight.collider)
        {
            onRightWall = true;
        }
        else
        {
            onRightWall = false;
        }
    }


    IEnumerator StopDash(float dashTime)
    {
        yield return new WaitForSeconds(dashTime);
        playerRb.velocity = new Vector2(0, 0);
        Physics2D.IgnoreLayerCollision(11, 12, ignore: false);
        isDashing = false;
        print("made 0");
    }
}

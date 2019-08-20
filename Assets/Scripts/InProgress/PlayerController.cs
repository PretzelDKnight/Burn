using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Stuff")]
    [SerializeField] float movementSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float timeBeforeNextDash;                  // The amount of time the player have to wait befoer he can dash again
    float dashGap = 0;                                          // The dah timer keeping track of how much time has passed since the previous dash
    [SerializeField] float jumpForce;
    [SerializeField] float jumpForceForward;                    // The horizontal push off force form the wall while wall jumping
    int jumpNumber = 2;                                         // The total number of jumps the player can perform in one lift off
    Rigidbody2D playerRb;

    [Header("Stuff required for RayCasting")]
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] LayerMask wallLayerMask;
    float halfHightVertical;                                    // The distance form the center of the player to the end of his model horizontally
    float halfHightHorizontal;                                  // The distance form the center of the player to the end of his model vertically

    [Header("Stuff required for animations")]
    [SerializeField] Animator animate;
    [SerializeField] float timeFactor;
    Quaternion currentRotation;
    Quaternion endRotation;
    bool turning;
    bool startGroundCheck;

    // Boolians
    bool grounded = true;                                       // To check if player is in contact with the floor
    bool notInAir = false;                                      // To check if he is in the air, i.e middle of this jump
    bool onRightWall = false;                                   // To check if the wall in contact with the player is on his right
    bool onLeftWall = false;                                    // To check if the wall in contact with the player is on his left
    bool lookingRight = true;                                   // To check if the player is looking to the right
    bool lookingLeft = false;                                   // To check if player is looking to the left
    bool isDashing = false;                                     // To check if player is in the middle of his dash
    bool recentlyDashed = false;                                // To check if the player has recently dashed



    void Start()
    {
        // Assigning funtions to each event called in the input manager
        InputManager.HahaVeryFunny += KyleHasDirtyUnderwear;
        InputManager.LeftAndRight += MoveForawrdOrBack;
        InputManager.Jumping += Jump;
        InputManager.Interacting += Interact;
        InputManager.Attacking += Attack;
        InputManager.Dash += Dashing;

        //Assigning values to the following variables
        halfHightVertical = transform.localScale.y / 2 +.1f;
        halfHightHorizontal = transform.localScale.x / 2 + .1f;

        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Making player velocity 0 to stop instantly as soon as button is released
        if (Input.GetAxisRaw("Horizontal") == 0 && notInAir)
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }

        // Checking if the player recently dashed if so starting the timer that makes dash availabe again when this timer hits 0
        if (dashGap > 0)
        {
            dashGap -= Time.deltaTime;
        }
        else if (dashGap <= 0)
        {
            recentlyDashed = false;
        }

        //For animations
        animate.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        if(startGroundCheck)
            RayCastingFunction();
        if(grounded && startGroundCheck)
        {
            animate.SetBool("Jump", false);
            startGroundCheck = false;
        }
        if(onLeftWall)
        {
            animate.SetBool("ToWall", true);
            StartCoroutine(WaitForAnimation("ToWall"));
        }
        else if (onRightWall)
        {
            animate.SetBool("ToWall", true);
            StartCoroutine(WaitForAnimation("ToWall"));
        }
    }



    // Function to check how many days Kyle is waring the same underwear
    void KyleHasDirtyUnderwear(float i)
    {
        Debug.Log("KYLE Y U DO DIS??!!" + " Kyle has spent " + i + " days in the same stinky underwear");
    }

    // Function to move the player on the platofrms
    void MoveForawrdOrBack()
    {
        RayCastingFunction();
        // Move at normal velocity if the player is not dashing
        if (!isDashing)
        {
            if (grounded)
                notInAir = true;

            if ((onLeftWall || onRightWall) && !grounded) ;

            else
                playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, playerRb.velocity.y);
        }
        //Turning 
        if(lookingRight && Input.GetAxisRaw("Horizontal") < 0)
        {
            turning = true;
            currentRotation = transform.rotation ;
            endRotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            StartCoroutine(TurningPlayer(currentRotation, endRotation));
        }
        else if (lookingLeft && Input.GetAxisRaw("Horizontal") > 0)
        {
            turning = true;
            currentRotation = transform.rotation;
            endRotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            StartCoroutine(TurningPlayer(currentRotation, endRotation));
        }
        // Setting which side the player is facing
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            lookingLeft = true;
            lookingRight = false;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            lookingLeft = false;
            lookingRight = true;
        }
    }

    // Function to make the player jump, double jump and wall jump
    void Jump()
    {
        RayCastingFunction();
        if (grounded)                                          // Make player jump if on the ground
        {
            animate.SetBool("Jump", true);
            StartCoroutine(GroundCheck());
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            jumpNumber = 1;
            notInAir = false;
        }
        else if(onLeftWall || onRightWall)                     // Make player jump of the wall if in contact with a wall
        {
            if(onRightWall)
            {
                playerRb.velocity = new Vector2(0, 0);
                turning = true;
                currentRotation = transform.rotation;
                endRotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                StartCoroutine(TurningPlayer(currentRotation, endRotation));
                lookingLeft = true;
                lookingRight = false;
                playerRb.AddForce(new Vector2(-jumpForceForward, jumpForce), ForceMode2D.Impulse);
            }
            else if (onLeftWall)
            {
                playerRb.velocity = new Vector2(0, 0);
                turning = true;
                currentRotation = transform.rotation;
                endRotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                StartCoroutine(TurningPlayer(currentRotation, endRotation));
                lookingRight = true;
                lookingLeft = false;
                playerRb.AddForce(new Vector2(jumpForceForward, jumpForce), ForceMode2D.Impulse);
            }
            notInAir = false;
        }
        else if(jumpNumber > 0)                                // Check if player has more jumps left if so then perform nother jump form the point he is at
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            jumpNumber--;
        }
    }

    //Function to make the player dash and not collide with enemis when he does so
    void Dashing()
    {
        if (!recentlyDashed)
        {
            Physics2D.IgnoreLayerCollision(11, 12, ignore: true);
            isDashing = true;
            animate.SetBool("Dash", true);
            StartCoroutine(StopDash(dashDuration));
            // Based on direction the player is looking at dash in that direction even if the player gives no direction input
            if (lookingLeft)
            {
                playerRb.velocity = new Vector2(-1 * dashSpeed, 0);
            }
            else if (lookingRight)
            {
                playerRb.velocity = new Vector2(1 * dashSpeed, 0);
            }
        }
    }


    // The function that makes the player interact with the object in front of him
    void Interact()
    {
        print("<color=pink> INTERACTING NOW</color>");
    }

    // The function that makes the player attack with his sword
    void Attack()
    {
        print("<color=cyan> ATTACKING NOW </color>");
    }


    //Ray Cast function where all raycasting from the player is handled
    void RayCastingFunction()
    {
        // Casting ray down to check if player is on the platform
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

        // Casting ray to the left to check if the player is in contact with the left wall
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

        // Casting ray to the left to check if the player is in contact with the right wall
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

    // Function needed to run as courotine to make dash stop after a given amout of time 
    IEnumerator StopDash(float dashTime)
    {
        yield return new WaitForSeconds(dashTime);                          // Waiting (time) for the dash to complete
        playerRb.velocity = new Vector2(0, 0);                              // Stoping the player at the end of the dash
        Physics2D.IgnoreLayerCollision(11, 12, ignore: false);              // Turing the collisons between enemy and player back ON
        isDashing = false;
        animate.SetBool("Dash", false);
        recentlyDashed = true;
        dashGap = timeBeforeNextDash;                                       // Setting the timer  counting down to the next dash
    }

    IEnumerator TurningPlayer(Quaternion start, Quaternion end)
    {
        yield return new WaitForSeconds(0.1f);
        float tick = 0; 
        while(turning)
        {
            if (tick >= 1)
            {
                turning = false;
                break;
            }
            tick += Time.deltaTime * timeFactor;
            transform.rotation = Quaternion.Lerp(start, end, tick);
        }
    }

    IEnumerator GroundCheck()
    {
        yield return new WaitForSeconds(0.5f);
        startGroundCheck = true;
    }

    IEnumerator WaitForAnimation(string boolName)
    {
        yield return new WaitForSecondsRealtime(1);
        animate.SetBool(boolName, false);
    }
}

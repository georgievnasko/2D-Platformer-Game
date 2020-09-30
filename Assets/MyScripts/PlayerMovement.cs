using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {

    public Animator playerAnimator;

    public GameObject playerEye;
    public GameObject playerMouth;

    private bool movingLeft = false;
    private bool movingRight = false;
    private float movingSpeed = 3.0f;

    public static int playerDirection = 1;

    private bool run = false;
   
    private float jumpingPower = 10.0f;
    private float jumpTime = 0.3f;
    private bool jump = false;
    private bool canJump = true;

    private Vector2 playerEyePositionRight;
    private Vector2 playerEyePositionLeft;
    private Vector2 playerMouthPositionRight;
    private Vector2 playerMouthPositionLeft;


    private void Start()
    {
        playerEyePositionRight = new Vector2(0.03f, 0.03f);
        playerEyePositionLeft = new Vector2(-0.03f, 0.03f);
        playerMouthPositionRight = new Vector2(0.03f, -0.03f);
        playerMouthPositionLeft = new Vector2(-0.03f, -0.03f);
    }


    void Update ()
    {
        playerDirection = getDirection();

        if (PlayerScript.playing)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                movingLeft = true;
                playerAnimator.SetTrigger("walk");
            }

            if (Input.GetKeyUp(KeyCode.A))
            { 
                movingLeft = false;
                playerAnimator.SetTrigger("idle");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                movingRight = true;
                playerAnimator.SetTrigger("walk");
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                movingRight = false;
                playerAnimator.SetTrigger("idle");
            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                jump = true;
                canJump = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                run = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                run = false;
            }


            // jumpTime float is a timer for the player to reach its highest point when jumping, after the time is up the player fall down to complete the jump effect
            if (jump)
            {
                jumpTime -= Time.deltaTime;
                transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * jumpingPower);
                if (jumpTime < 0f)
                {
                    jumpTime = 0.3f;
                    jump = false;
                }
            }

            movingSpeed = run ? 6.0f : 3.0f;

            if (movingLeft)
            {
                MoveLeft(transform.position, movingSpeed);

                playerEye.transform.localPosition = playerEyePositionLeft;
                playerMouth.transform.localPosition = playerMouthPositionLeft;
            }
            if (movingRight)
            {
                MoveRight(transform.position, movingSpeed);

                playerEye.transform.localPosition = playerEyePositionRight;
                playerMouth.transform.localPosition = playerMouthPositionRight;
            }
        }
        else
        {
            playerAnimator.SetTrigger(PlayerScript.finished ? "idle" : "die");
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            canJump = true;
        }

        if(collision.gameObject.name == "Finish")
        {
            PlayerScript.playing = false;
            PlayerScript.finished = true;
        }

        if (collision.gameObject.name == "EnemyProjectile(Clone)")
        {
            Destroy(collision.gameObject);
            PlayerScript.health--;
        }
    }
}

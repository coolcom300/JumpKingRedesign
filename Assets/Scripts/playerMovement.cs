using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Cryptography;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public PhysicsMaterial2D bounceMat, groundMat;

    public float jumpPowerMax;
    public float sidejumpPowerMax;
    public float jumpPower;
    public float timer;
    public float walkSpeed;
    public bool canJump;
    public bool faceingRight;
    public float playerOffSetX;
    public float playerOffSetY;
    public float chargeTime;
    bool atMaxJump;
    public float height;
    public float walkOffSpeed;

    bool enter = false;

    Rigidbody2D body;
    Transform trans;
    SpriteRenderer spriteRend;

    bool canWalkR = true;
    bool canWalkL = true;

    bool once;
    //Vector3 initialGravity = Physics2D.gravity;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        spriteRend = GetComponent<SpriteRenderer>();
        GetOffSet();
        


    }

    // Update is called once per frame
    void Update()
    {

        Walk();
        SpriteStuff();
        Jump();
        // changeGravity();


    }

    //Start of Created Methods/Functions

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != "Player")
        {
            if (canJump == false)
            {
                body.sharedMaterial = bounceMat;
                UnityEngine.Debug.Log("enter");
                UnityEngine.Debug.Log(body.sharedMaterial);
            }

        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //string tagVar = collision.Get();
        if (collision.tag != "Player")
        {
            body.sharedMaterial = groundMat;
            UnityEngine.Debug.Log("exit");
            UnityEngine.Debug.Log(body.sharedMaterial);
        }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (body.sharedMaterial == groundMat)
        {
            body.velocity = new Vector3(0f, 0f, 0f);
            canJump = true;
            canWalkR = true;
            canWalkL = true;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        //UnityEngine.Debug.Log("col stay");


    }

    void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;

    }

    void GetOffSet()// gets the players offset for other methods
    {
        playerOffSetX = trans.localScale.x / 2;
        playerOffSetY = trans.localScale.y / 2;
    }

    void SpriteStuff()//for rendering sprites
    {
        if (faceingRight)
        {
            spriteRend.flipX = false;
        }
        if (!faceingRight)
        {
            spriteRend.flipX = true;
        }

    }

    void Walk()//the walk function
    {
        if (canJump)
        {
            enter = false;
            if (Input.GetKey(KeyCode.D))// D to move right at walkSpeed
            {
                if (canWalkR == true)
                {
                    trans.position += transform.right * walkSpeed * Time.deltaTime;
                    //if(trans.position.x > -80f){
                    //    canWalkR = false;
                    //    UnityEngine.Debug.Log("f");
                    //}
                    faceingRight = true;
                    trans.rotation = Quaternion.Euler(0, 0, 0);
                    canWalkL = true;
                    enter = true;
                }

            }
            if (Input.GetKey(KeyCode.A))// A to move -right at walkSpeed
            {
                if (canWalkL == true)
                {
                    trans.position += -transform.right * walkSpeed * Time.deltaTime;
                    faceingRight = false;
                    trans.rotation = Quaternion.Euler(0, 0, 0);
                    canWalkR = true;
                    enter = true;
                }
            }
            once = true;
        }

        //4 works for walk off speed
        else
        {
            if (faceingRight && enter && once)
            {

                body.AddForce(transform.right * walkOffSpeed, ForceMode2D.Impulse);
                once = false;
            }

            if (!faceingRight && enter && once)
            {
                body.AddForce(-transform.right * walkOffSpeed, ForceMode2D.Impulse);
                once = false;
            }

        }


    }

    void SetZero()//sets some values to zero to prevent sliding || or rotating
    {
        body.rotation = 0;
        body.angularVelocity = 0;
        trans.rotation = Quaternion.Euler(0, 0, 0);

    }


    void Jump()//the jump function
    {
        if (Input.GetKeyUp(KeyCode.Space) || atMaxJump)
        {
            //enter = true;

            if (timer > chargeTime)//sets for max
            {
                timer = chargeTime;
            }
            jumpPower = timer / chargeTime;


            body.AddForce(transform.up * jumpPowerMax * jumpPower, ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.D))
            {

                body.AddForce(transform.right * sidejumpPowerMax * jumpPower, ForceMode2D.Impulse);
                faceingRight = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(-transform.right * sidejumpPowerMax * jumpPower, ForceMode2D.Impulse);
                faceingRight = false;
            }
            canJump = false;
            atMaxJump = false;
            timer = 0;
            UnityEngine.Debug.Log("jump");
        }




    }

    void ChargeJump()//charges Jump
    {
        if (canJump)
        {
            timer += Time.deltaTime;
            if (timer >= chargeTime)
            {
                atMaxJump = true;
                Jump();
            }
            canWalkL = false;
            canWalkR = false;
        }
    }

    void FixedUpdate()// uses jump
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ChargeJump();
        }

    }

    void changeGravity()
    {
        // height = body.position.y;
        //height = height

        // Physics2D.gravity = new Vector2(0, -5);




        // body.gravityScale = 1;
    }
}





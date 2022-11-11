using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float jumpPowerMax;
    public float jumpPower;
    public float timer;
    public float walkSpeed;
    public bool canJump;
    public bool faceingRight;
    public float playerOffSetX;
    public float playerOffSetY;
    public float chargeTime;
    bool atMaxJump;
    Rigidbody2D body;
    Transform trans;
    SpriteRenderer spriteRend;

    bool canWalkR = true;
    bool canWalkL = true;

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
        //SetZero();
        Walk();
        SpriteStuff();
        Jump();
    }
    
//Start of Created Methods/Functions

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Testing git @coolcom200
        //UnityEngine.Debug.Log("it is touching");//this should be deleted at the release stage
        float offSetY = collision.gameObject.transform.localScale.y/2;
        float collPosY = collision.gameObject.transform.position.y;
        float offSetX = collision.gameObject.transform.localScale.x / 2;
        float collPosX = collision.gameObject.transform.position.x;
        if (trans.position.y > collPosY + offSetY)
        {
            //trans.position = new Vector2(trans.position.x,collPosY + offSetY);
            //trans.position.y == collPosY + offSetY + playerOffSetY;
            //trans.position.x = collPosX + offSetX + playerOffSetX;
            canJump = true;
            canWalkR = true;
            canWalkL = true;

        }
        if (!canJump)
        {
            if (trans.position.x > collPosX + offSetX)
            {
                body.AddForce(transform.right * jumpPowerMax / 2, ForceMode2D.Impulse);
                UnityEngine.Debug.Log("enter bounce");
                //body.velocity = new Vector2(-body.velocity.x,body.velocity.y);
            }
            if (trans.position.x < collPosX - offSetX)
            {
                body.AddForce(-transform.right * jumpPowerMax / 2, ForceMode2D.Impulse);
                UnityEngine.Debug.Log("enter bounce");
                //body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
                //body.velocity.x = -body.velocity.x;
            }
        }
        if (trans.position.x < collPosX - offSetX)//Right walk off
        {
            canWalkR = false;

        }

        if (trans.position.x > collPosX + offSetX)//Left walk off
        {
            canWalkL = false;


        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        canJump = true;
    }
    //{
    //    float offSetX = collision.gameObject.transform.localScale.x / 2;
    //    float collPosX = collision.gameObject.transform.position.x;
    //    if (trans.position.x > collPosX + offSetX)
    //    {
    //        body.AddForce(transform.right * jumpPowerMax / 2, ForceMode2D.Impulse);
    //        UnityEngine.Debug.Log("stay bounce");
    //        //body.velocity = new Vector2(-body.velocity.x,body.velocity.y);
    //    }
    //    if (trans.position.x < collPosX - offSetX)
    //    {
    //        body.AddForce(-transform.right * jumpPowerMax / 2, ForceMode2D.Impulse);
    //        UnityEngine.Debug.Log("stay bounce");
    //        //body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
    //        //body.velocity.x = -body.velocity.x;
    //    }
    //}

    void OnCollisionExit2D(Collision2D collision)
    {

        float offSetX = collision.gameObject.transform.localScale.x / 2;
        float collPosX = collision.gameObject.transform.position.x;
        if (trans.position.x < collPosX - offSetX)//Right
        {
            canWalkR = true;

        }

        if (trans.position.x > collPosX + offSetX)//Left
        {
            canWalkL = true;


        }
        //float offSetY = collision.gameObject.transform.localScale.y / 2;
        //float collPosY = collision.gameObject.transform.position.y;
        //if (trans.position.y > collPosY - offSetY) 
        //{ 
            canJump = false;
        //}
    }

    void GetOffSet()
    {
        playerOffSetX = trans.localScale.x / 2;
        playerOffSetY = trans.localScale.y / 2;
        //return playerOffSetX;
        //return playerOffSetY;
    }

    void SpriteStuff()
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

    void Walk()
    {
        if (canJump) 
        { 
            if (Input.GetKey(KeyCode.D))// D to move right at walkSpeed
            {
                if (canWalkR == true)
                {
                    trans.position += transform.right * walkSpeed * Time.deltaTime;
                    faceingRight = true;
                    trans.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.A))// A to move -right at walkSpeed
            {
                if (canWalkL == true)
                {
                    trans.position += -transform.right * walkSpeed * Time.deltaTime;
                    faceingRight = false;
                    trans.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }

    void SetZero()//sets some values to zero to prevent sliding || or rotating
    {
        body.rotation = 0;
        body.angularVelocity = 0;
        trans.rotation = Quaternion.Euler(0, 0, 0);
        if (canJump)
        {
            body.velocity = new Vector2(0,body.velocity.y);
        }
        
    }


    void Jump()
    {
        if (Input.GetKeyUp(KeyCode.Space)|| atMaxJump)
        {
            if (timer > chargeTime)//sets for max
            {
                timer = chargeTime;
            }
            jumpPower = timer / chargeTime;
        
        
            body.AddForce(transform.up * jumpPowerMax * jumpPower, ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(transform.right * 1.5f * jumpPowerMax * jumpPower, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(-transform.right * 1.5f * jumpPowerMax * jumpPower, ForceMode2D.Impulse);
            }
            canJump = false;
            atMaxJump = false;
            timer = 0;
        }

        
    }

    void ChargeJump()
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
}

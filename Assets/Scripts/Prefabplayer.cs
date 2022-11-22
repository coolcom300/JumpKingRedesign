using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabplayer : MonoBehaviour
{
    [SerializeField] Component rightCol;
    [SerializeField] Component leftCol;
    [SerializeField] Component bottomCol;

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
        Walk();
        SpriteStuff();
        Jump();
        CheckTriggers();
    }
    
    void FixedUpdate()// uses jump
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ChargeJump();
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

    void Jump()
    {
        if (Input.GetKeyUp(KeyCode.Space) || atMaxJump)
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
                    canWalkL = true;
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
                }
            }
        }
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

    void GetOffSet()
    {
        playerOffSetX = trans.localScale.x / 2;
        playerOffSetY = trans.localScale.y / 2;
    }

    void OnCollisionExit2D()
    {
        canJump = false;
    }

    void CheckTriggers()
    {
        if (bottomCol)
        {
            canJump = true;
        }
        if (leftCol)
        {
            if (canJump == false)
            {
                body.AddForce(-transform.right * body.velocity.x / 2, ForceMode2D.Impulse);
            }
            canWalkL = false;
        }
        if (rightCol)
        {
            if (canJump == false)
            {
                body.AddForce(transform.right * body.velocity.x / 2, ForceMode2D.Impulse);
            }
            canWalkR = false;
        }
    }
}

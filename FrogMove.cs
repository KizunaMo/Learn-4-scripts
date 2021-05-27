using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    
    

    [Header("青蛙狀態確認")]
    public bool jumped,falling,doubleJumping;
    public bool isOnGround;
    public GameObject groundCheck;
    public LayerMask platform;

    [Header("玩家按鍵確認")]
    public bool jumpPressed,walkPressed;
    public int jumpCount;

    [Header("青蛙運動確認")]
    float xVelocity;
    public float walkSpeed = 300f;
    public float jumpForce = 300f;
    public int FallGracity;
    public float checkRadius;

    [Header("動畫設定")]
    int runID, hitID, jumpID, isOnGroundID,doubleJumpID;


    [Header("青蛙包包")]
    bool openBagPressed;
    bool isOpen;
    public GameObject frogGag;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        runID = Animator.StringToHash("horizontal");
        jumpID = Animator.StringToHash("Yvelocity");
        isOnGroundID = Animator.StringToHash("isOnGround");
        doubleJumpID = Animator.StringToHash("doubleJump");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            walkPressed = true;
        }
        if (Input.GetButtonDown("Vertical") && jumpCount >0)
        {
            jumpPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            openBagPressed = true;
        }
      
    }
    private void FixedUpdate()
    {
        Movement();
        FaceDirect();
        Jump();
        Fall();
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, platform);
        SwitchAnimation();
        OpenBag();
    }
    void Movement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        if (walkPressed)
        {
            rb.velocity = new Vector2(xVelocity * walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
            walkPressed = false;
        }
            
    }
    void Jump()
    {
        if (isOnGround)
        {
            jumpCount = 1;
            jumped = false;
            doubleJumping = false;
        }

        if (jumpPressed && isOnGround)
        {
            jumped = true;
            rb.AddForce(new Vector2(0, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && !isOnGround && jumpCount > 0)
        {
            doubleJumping = true;
            rb.AddForce(new Vector2(0, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            jumpCount--;
            jumpPressed = false;
        }
    }
    void Fall()
    {
        if (rb.velocity.y < -2)
        {
            falling = true;
            rb.gravityScale = FallGracity;
        }
        if (rb.velocity.y > 0)
        {
            falling = false;
            rb.gravityScale = 3f;
        }
    }
    void FaceDirect()
    {
        if(xVelocity != 0)
        {
            rb.transform.localScale = new Vector3(xVelocity, 1, 1);
        }
    }
      private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkRadius);
    }
    void SwitchAnimation()
    {
        anim.SetFloat(runID, Mathf.Abs(rb.velocity.x));
        anim.SetFloat(jumpID, rb.velocity.y);
        anim.SetBool(isOnGroundID, isOnGround);
        anim.SetBool(doubleJumpID, doubleJumping);
    }
    void OpenBag()
    {
        if (openBagPressed)
        {
            isOpen = !isOpen;
            frogGag.SetActive(isOpen);
            openBagPressed = false;
        }
    }
}

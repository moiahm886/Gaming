using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour{
    public Rigidbody2D rb;
    private Animator animate;
    private float WalkSpeed;
    private float RunSpeed;
    public bool IsGround;
    private SpriteRenderer Sprite;
    private float JumpSpeed;
    public float horizontalMovement;
    public float verticalMovement;
    public bool attack;
    public bool runKeyPressed;
    public bool Dead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        animate = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        WalkSpeed = 2f;
        RunSpeed = 5f;
        JumpSpeed = 20f;
        IsGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        runKeyPressed = Input.GetKey("right shift");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        Dead = Input.GetKey(KeyCode.M);
        verticalMovement = Input.GetAxisRaw("Vertical");
        attack = Input.GetKey(KeyCode.X);
    }
    void FixedUpdate()
    {
         if(horizontalMovement>0.1f){
            Sprite.flipX = false;
            animate.SetBool("isWalking",true);
            rb.AddForce(new Vector2(horizontalMovement*WalkSpeed,0f),ForceMode2D.Impulse);
        }
        else if(horizontalMovement<-0.1f){
            Sprite.flipX = true;
            animate.SetBool("isWalking",true);
            rb.AddForce(new Vector2(horizontalMovement*WalkSpeed,0f),ForceMode2D.Impulse);
        }
        else{
            animate.SetBool("isWalking",false);
        }
        if(runKeyPressed){
            if(horizontalMovement>0.1f){
            Sprite.flipX = false;
            animate.SetBool("isRunning",true);
            rb.AddForce(new Vector2(horizontalMovement*RunSpeed,0f),ForceMode2D.Impulse);
            }
            else if(horizontalMovement<-0.1f){
                Sprite.flipX = true;
                animate.SetBool("isRunning",true);
                rb.AddForce(new Vector2(horizontalMovement*RunSpeed,0f),ForceMode2D.Impulse);
            }
        }
        else{
            animate.SetBool("isRunning",false);
        }
        if(attack)
        {
            animate.SetBool("isAttack",true);
        }
        else
        {
            animate.SetBool("isAttack",false);
        }
        if(Dead)
        {
            animate.SetBool("isDead",true);
        }
        else
        {
            animate.SetBool("isDead",false);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0,Sprite.bounds.extents.y+0.01f,0),Vector2.down,0.1f);
        if(hit)
        {
            IsGround = true;
        }
        else{
            IsGround = false;
        }
        if(verticalMovement>0.1f && IsGround)
        {
            rb.AddForce(new Vector2(0f,verticalMovement*JumpSpeed),ForceMode2D.Impulse);
            animate.SetBool("Jump",true);
        }
        else
        {
            animate.SetBool("Jump",false);
        }
    }
}

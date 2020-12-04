using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 jumpForce;
    public float speed, jump;
    Animator anim;
    bool grounded = false;
    float groundRadius = .3f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Slider slider;
    public int maxHealth = 90;    
    public static int health;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForce = new Vector2(0f, jump);
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rb.velocity.y);
        Movement();
        Health(health);
    }

    private void Update()
    {        
        Jumping();
    }

    private void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(input * speed / 100, 0f, 0f);
        if (input < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetFloat("Speed", -1f * input);
        }
        else if (input > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            anim.SetFloat("Speed", input);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    private void Jumping()
    {                
        if (Input.GetButtonDown("Jump") && grounded)
        {
            anim.SetBool("Ground", false);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }       
    }

    public void Health(int health)
    {
        slider.value = health;
    }

}
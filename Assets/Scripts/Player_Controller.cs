using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed = 0.5f;
    private Vector2 moveVector;
    private bool flipRight = true;
    public float jumpForce = 2f;
    private Animator anim;
    public bool isGrounded;
    public Vector2 groundCheckSize;
    private float gravitydirection = 0;
    public GameObject Camera;
    public GameObject Menu;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.CompareTag("Finish"))
        {
            EndLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        float Xmove = Input.GetAxis("Horizontal");

        //isGrounded = Physics2D.OverlapBox(transform.position, groundCheckSize, 0, LayerMask.GetMask("Ground"));

        if ((Xmove > 0 && !flipRight) || (Xmove < 0 && flipRight))
            flip();

        bool addclick = Input.GetKeyDown(KeyCode.Q);
        bool minclick = Input.GetKeyDown(KeyCode.E);

        if (addclick)
        {
            gravitydirection += 1;

            if (gravitydirection >= 4)
                gravitydirection = 0;
        }
        if (minclick)
        {
            gravitydirection -= 1;

            if (gravitydirection < 0)
                gravitydirection = 3;
        }
        switch (gravitydirection)
        {
            case 0:
                Physics2D.gravity = new Vector2(0, -9.8f); 
                transform.eulerAngles = new Vector3(0, 0, 0);
                Camera.transform.eulerAngles = new Vector3(0, 0, 0);
                gravitydirection = 0;
            break;
            case 1:
                Physics2D.gravity = new Vector2(-9.8f, 0);
                transform.eulerAngles = new Vector3(0, 0, -90);
                Camera.transform.eulerAngles = new Vector3(0, 0, -90);
                gravitydirection = 1;
                break;
            case 2:
                Physics2D.gravity = new Vector2(0, 9.8f);
                transform.eulerAngles = new Vector3(0, 0, 180);
                Camera.transform.eulerAngles = new Vector3(0, 0, 180);
                gravitydirection = 2;
                break;
            case 3:
                Physics2D.gravity = new Vector2(9.8f, 0);
                transform.eulerAngles = new Vector3(0, 0, 90);
                Camera.transform.eulerAngles = new Vector3(0, 0, 90);
                gravitydirection = 3;
                break;
        }
                
                
            


        

        switch (gravitydirection)
        {
            case 0:
                rb.velocity = new Vector2(Xmove * walkSpeed, rb.velocity.y);
                break;
            case 1:
                rb.velocity = new Vector2(rb.velocity.x, -Xmove * walkSpeed);
                break;
            case 2:
                rb.velocity = new Vector2(-(Xmove * walkSpeed), rb.velocity.y);
                break;
            case 3:
                rb.velocity = new Vector2(rb.velocity.x, Xmove * walkSpeed);
                break;
        }

        bool Esc = Input.GetKeyDown(KeyCode.Escape);

        if (Esc)
        {
            Menu.active = !Menu.active;
        }

        //transform.position += new Vector3(Xmove * walkSpeed * Time.deltaTime, 0, 0);
        //transform.Translate(Vector3.right * walkSpeed * Xmove * Time.deltaTime);
        //rb.AddForce(new Vector2(walkSpeed * Xmove, 0));

        
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            switch (gravitydirection)
            {
                case 0:
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    break;
                case 1:
                    rb.velocity = new Vector2(jumpForce, rb.velocity.y);
                    break;
                case 2:
                    rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
                    break;
                case 3:
                    rb.velocity = new Vector2(-jumpForce, rb.velocity.y);
                    break;
            }

        }
        anim.SetBool("Ground", isGrounded);
    }

    private void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.loadedSceneCount + 1);
    }

    private void flip()
    {
        flipRight = !flipRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

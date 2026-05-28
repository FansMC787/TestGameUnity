using System;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    private float horizontale;
    private float speed = 8f;
    private float jumpingpower = 8f;
    private bool guckterrechts = true;
    private int Sprüngeübrig = 1;
    private int Dashcounter = 1;
    private float Dashpower = 20f;
    private float dashTimer;
    private void OnEnable() => sprintAction.Enable();
    private void OnDisable() => sprintAction.Disable();

    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private InputAction sprintAction;
    

   

    // Update is called once per frame
    void Update()
    {
        horizontale = Input.GetAxisRaw("Horizontal");

        if (isgrounded())
        {
            Sprüngeübrig = 1;
            
        }

        
        if (isgrounded())
        {
            Dashcounter = 1;
            
        }
        Jump();
        Dash();
        Flip();
    }

    private void FixedUpdate()
    {
        Timer();
    }
    private bool isgrounded ()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }
    
    private void Dash()
    {
       if (sprintAction.WasPressedThisFrame() && (isgrounded () || Dashcounter > 0))
        {
            rb.linearVelocity = new Vector2 (horizontale * Dashpower, rb.linearVelocity.y);
            dashTimer = 0.2f;
            Dashcounter--;
            Debug.Log("Dash erkannt");
        }
    
    }



   private void Jump()
    {
       if (Input.GetButtonDown("Jump") && (isgrounded () || Sprüngeübrig > 0))
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, jumpingpower);
            Sprüngeübrig--;
            Debug.Log("Sprunge Übrig: " + Sprüngeübrig);
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        } 
       
    }

private void Timer()
    {
        if (dashTimer > 0)
    {
        
        dashTimer -= Time.fixedDeltaTime;
    }
    else
    {
        
        rb.linearVelocity = new Vector2(horizontale * speed, rb.linearVelocity.y);
    }
    }


    private void Flip()
    {
        if (guckterrechts && horizontale < 0f || !guckterrechts && horizontale > 0f )
        {
            guckterrechts = !guckterrechts;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

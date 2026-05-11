using System;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private float horizontale;
    private float speed = 8f;
    private float jumpingpower = 10f;
    private bool guckterrechts = true;
    private int Sprüngeübrig = 2;

    

    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform Doublejump;
    [SerializeField] private LayerMask GroundLayer;
    

   

    // Update is called once per frame
    void Update()
    {
        horizontale = Input.GetAxisRaw("Horizontal");
        
        Jump();
        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontale * speed, rb.linearVelocity.y);
    }
    private bool isgrounded ()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
        Sprüngeübrig++;
        Console.WriteLine("+1");
    }
    
   private void Jump()
    {
       if (Input.GetButtonDown("Jump") && isgrounded () )
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, jumpingpower);
            Sprüngeübrig--;
            Console.WriteLine("-1");
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
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

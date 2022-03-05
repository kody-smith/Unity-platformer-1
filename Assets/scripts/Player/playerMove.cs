using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private float wallJumpCooldown;
    private Rigidbody2D body;
    private Animator ani;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    [Header ("SFX Sounds")]
    [SerializeField] private AudioClip jumpClip;


    private void Awake()
    {
        //Get reference
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);
    // Change direction(right)
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
    // Change direction(left)
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        // Set animation parameters
        ani.SetBool("Run", horizontalInput != 0);
        ani.SetBool("grounded", isGrounded());

        //Wall jump logic
        if(wallJumpCooldown > 0.1f)
        {

            body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            
            else
                body.gravityScale = 7;
            
            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                SoundManager.instance.Play(jumpClip);
            }
                
        }
        else
            wallJumpCooldown += Time.deltaTime;

        
    }
    private void Jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x,jumpSpeed);
            ani.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }

    }
    private bool isGrounded()
    {
        RaycastHit2D hit =  Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D hit =  Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return hit.collider != null;
    }
    public bool canAttack()
    {
        return true;
    }
}


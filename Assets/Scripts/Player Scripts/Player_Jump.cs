using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Jump : MonoBehaviour
{
    [SerializeField] private float baseJumpHeight = 3f;
    [SerializeField] private float fallSpeedMultiplier = 2.5f;
    [SerializeField] private float heldJumpMultipler = 2f;
    [SerializeField] private float terminalFallSpeed = 50f;
    private bool isJumping = false;
    private float gravity = -9.81f;
    private Rigidbody2D rb;
    private Player_Dash dashScript;
    [SerializeField] Animator animator;
    private BoxCollider2D playerHitBox;

    //sound
    public AudioClip jumpSFX;
    public float volume = 0.9f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHitBox = GetComponent<BoxCollider2D>();
        // get the gravity value for calculating jump height
        gravity = Physics2D.gravity.y;
        gravity *= rb.gravityScale;
        
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dashScript = GetComponent<Player_Dash>();
        
    }


    void playJumpSound()
    {
        AudioSource.PlayClipAtPoint(jumpSFX, transform.position, volume);
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded() && !dashScript.getIsDashing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Sqrt(baseJumpHeight * -2f * gravity));
            isJumping = true;
            animator.SetTrigger("Jump Start");
            playJumpSound();
        }
        if(context.canceled && !dashScript.getIsDashing)
        {
                isJumping = false;
            
        }

    }
    public bool isInAir()
    {
        return !isGrounded();
    }
    public bool GetisGrounded()
    {
        return isGrounded();
    }
    public bool getIsJumping()
    {
        return isJumping;
    }
    public bool getIsFalling()
    {
        return rb.linearVelocityY < 0;
    }
    bool isGrounded()
    {
        bool Grounded = playerHitBox.IsTouchingLayers(LayerMask.GetMask("Floor")) ||  playerHitBox.IsTouchingLayers(LayerMask.GetMask("Enemy")) || playerHitBox.IsTouchingLayers(LayerMask.GetMask("Slippery")) || playerHitBox .IsTouchingLayers(LayerMask.GetMask("Platform"));
       
      
     
        return Grounded;
    }

        // Update is called once per frame
        void Update()
       {
        bool groundCheck = isGrounded();
       
        
        animator.SetBool("Is in air", !groundCheck);
        animator.SetBool("Is grounded", groundCheck);
       }
    private void FixedUpdate()
    {
        // if the player is falling
        if (rb.linearVelocityY < 0 && !dashScript.getIsDashing)
        {
            // calculate the new velocity by applying the fall speed multiplier to the gravity and adding it to the current velocity
            Vector2 currentVelocity = rb.linearVelocity;
            currentVelocity.y += gravity * (fallSpeedMultiplier - 1) * Time.fixedDeltaTime;
            // cap the velocity to the terminal fall speed
            currentVelocity.y = Mathf.Max(currentVelocity.y, -terminalFallSpeed);
            // set the new velocity
            rb.linearVelocity = currentVelocity;
            
        }


        // if the player is rising and the jump button is held, apply the held jump multiplier to the gravity to create a variable jump height
        else if (rb.linearVelocityY > 0 && !isJumping && !dashScript.getIsDashing)
        {
            // calculate the new velocity by applying the held jump multiplier to the gravity and adding it to the current velocity
            Vector2 currentVelocity = rb.linearVelocity;
            currentVelocity.y += gravity * (heldJumpMultipler - 1) * Time.fixedDeltaTime; 
            // cap the velocity to the terminal fall speed
            currentVelocity.y = Mathf.Max(currentVelocity.y, -terminalFallSpeed);
            // set the new velocity
            rb.linearVelocity = currentVelocity;
           
        }
    }
}


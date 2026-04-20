using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_Dash : MonoBehaviour
{
    [SerializeField] float dashDurationInSeconds;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashRechargeSpeed;
    [SerializeField] float dashHitboxRadius;
    [SerializeField] Image dashUITracker;
    [SerializeField] Sprite[] dashUIArray;
   
    [SerializeField] Animator animator;
    private const int maxDashStamina = 8;
    private int dashStamina = maxDashStamina;
    private const int dashCost = 2;
    Player_Movement player_Movement;
    private bool dashCancelled = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    private BoxCollider2D normalHitbox;
    private CircleCollider2D dashHitbox;
    private Rigidbody2D rb;
    private Vector2 originalHitboxSize;
    private bool isDashing = false;
    private float originalGravityScale;
    private float dashCooldownTimer = 0f;
    private bool canDash => dashStamina >= dashCost;
    public bool getIsDashing => isDashing;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        normalHitbox = GetComponent<BoxCollider2D>();
        dashHitbox = GetComponent<CircleCollider2D>();
        dashHitbox.radius = dashHitboxRadius;
        dashHitbox.enabled = false;
        originalGravityScale = rb.gravityScale;
        originalHitboxSize = transform.localScale;
        

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
   
        changeUISprite();
    }

    // Update is called once per frame
    void Update()
    {
        //tracks the cooldown timer for the dash
        dashCooldownTimer += Time.deltaTime;
        if (dashCooldownTimer >= dashRechargeSpeed)
        {
            if (dashStamina == maxDashStamina)
            {
                return;
            }
            else
            {
                dashStamina++;
                dashCooldownTimer = 0f;
                changeUISprite();
            }

        }
    }
    public void dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && !isDashing)
        {
            StartCoroutine(dash());
            dashStamina -= dashCost;
            changeUISprite();
        }
    }
    IEnumerator dash()
    {
        // set he player to dashing
        isDashing = true;
        dashCancelled = false;
        player_Movement.canMove = false;
        animator.SetTrigger("Dash start");
        
        // disable the normal hitbox and enable the dash hitbox
        normalHitbox.enabled = false;
        dashHitbox.enabled = true;
        

        // set the gravity scale to 0 so the player doesn't fall while dashing
        rb.gravityScale = 0;
        float dashTimer = 0f;


        // make the player smaller during the dash 
       

        // dash in the direction the player is facing for the duration of the dash

        //first get a noramlised dash direction 
        float Direction = player_Movement.getMoveInput.x;

        if (Direction == 0)
        {
            Direction = player_Movement.getPlayerFacingDirection;

        }

        // reset the linear velocity to 0 before dashing to ensure a consistent dash speed
        rb.linearVelocity = Vector2.zero;

        // dash in the direction for the duration of the dash
        while (dashTimer < dashDurationInSeconds && !dashCancelled)
        {
            rb.linearVelocity = new Vector2(Direction * dashSpeed, 0);
            dashTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // after the time for the dash is up, reset the player's velocity and hitbox and set the player back to normal
        // reset the player's velocity to 0 and set the player back to normal
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Dash end");
        normalHitbox.enabled = true;
        dashHitbox.enabled = false;
        rb.gravityScale = originalGravityScale;
        transform.localScale = originalHitboxSize;
        isDashing = false;
        player_Movement.canMove = true;
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            dashCancelled = true;
        }
 
    }

    void changeUISprite()
    {
        int index = Mathf.Clamp(dashStamina, 0, dashUIArray.Length - 1);
        dashUITracker.sprite = dashUIArray[index];
    }

}
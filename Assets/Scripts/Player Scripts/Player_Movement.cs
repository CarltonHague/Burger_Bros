using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float airMoveSpeed = 2f;
    [SerializeField] private float maxAirMoveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Player_Jump Player_Jump;

    private int playerFacingDirection = 1;

    public bool canMove = true;
    public int getPlayerFacingDirection => playerFacingDirection;
    public Vector2 getMoveInput => moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Player_Jump = GetComponent<Player_Jump>();


    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {
        // if the player is not allowed to move, don't process movement input
        if (!canMove)
        {
            return;
        }
        // if the player is on the ground allow them to do left right movement at full speed
        if (!Player_Jump.isInAir())
        {
            rb.linearVelocityX = (moveInput.x * speed);

        }
        // if the player is in the air, allow them to move but with less control and a max speed
        else if (Player_Jump.isInAir())
        {
            float newAirMoveSpeed = rb.linearVelocityX + (moveInput.x * airMoveSpeed) * Time.fixedDeltaTime;
            newAirMoveSpeed = Mathf.Clamp(newAirMoveSpeed, -maxAirMoveSpeed, maxAirMoveSpeed);
            rb.linearVelocityX = newAirMoveSpeed;
        }
    }


    public void move(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();

        if(moveInput.x != 0)
        {
            playerFacingDirection = (int)Mathf.Sign(moveInput.x);
        }
        
    }

}
    
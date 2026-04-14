using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Grapple : MonoBehaviour
{
    [SerializeField] Grapple_detector grappleDetector;
    [SerializeField] float grapplePullSpeed;
    [SerializeField] float grappleStopDistance;
    private DistanceJoint2D grappleJoint;
    private LineRenderer grappleLine;
    private Rigidbody2D playerRb;
    bool isGrappling = false;
    private Player_Movement movement;
    private void Awake()
    {
        grappleJoint = GetComponent<DistanceJoint2D>();
        grappleLine = GetComponent<LineRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        grappleLine.positionCount = 2;
        grappleJoint.maxDistanceOnly = true;
        grappleJoint.enabled = false;
        grappleLine.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        drawRope();
    }
    public void grapple(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
       
        if (isGrappling)
        {
            stopGrapple();
            
        }
        else
        {
            startGrapple();
            
        }

    }
    void startGrapple()
    {
       
        GrapplePoint point = grappleDetector.closestGrapplePoint;
        if(point == null && grappleDetector.grapplePointsInRange.Count > 0)
        {
            point = grappleDetector.grapplePointsInRange[0];
        }
        if (point == null)
        {
            return;
        }
        

        isGrappling = true;
        grappleJoint.enabled = true;
        movement.canMove = false;
        grappleJoint.connectedAnchor = point.transform.position;
        grappleJoint.distance = Vector2.Distance(transform.position, point.transform.position) * 0.8f;

    }

    void drawRope()
    {
        if(!isGrappling)
        {
            grappleLine.enabled = false;
            return;
        }
        grappleLine.enabled = true;
        grappleLine.SetPosition(0, transform.position);
        grappleLine.SetPosition(1, grappleJoint.connectedAnchor);
    }
    void stopGrapple()
    {
        isGrappling = false;
        grappleJoint.enabled = false;
        grappleLine.enabled = false;
        movement.canMove = true;
    }
    private void FixedUpdate()
    {
        // check the player is grappling 
        if (!isGrappling)
        {
            return;
        }
        // get the position of the grapple point 
        Vector2 target = grappleJoint.connectedAnchor;

        // get the vector from the player to the grapple point as a normalized direction vector
        Vector2 direction = (target - playerRb.position).normalized;

        // move the player in the direction using the pull speed 
        playerRb.linearVelocity = direction * grapplePullSpeed;

        // check the distance to the grapple point 
        float distance = Vector2.Distance(playerRb.position, target);

        // if the distance is less than the stop distance, stop the grapple and set the player's velocity to 0
        if (distance < grappleStopDistance)
        {
            stopGrapple();
            playerRb.linearVelocity = Vector2.zero;
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

public class Grapple_detector : MonoBehaviour
{
    [SerializeField] float grappleSearchRadius = 10f;
    CircleCollider2D playerSearchRange;
    public List<GrapplePoint> grapplePointsInRange = new List<GrapplePoint>();
    public GrapplePoint closestGrapplePoint;


    private void Awake()
    {
        playerSearchRange = GetComponent<CircleCollider2D>();
        playerSearchRange.radius = grappleSearchRadius;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        findClosestToTheMouse();
    }
    // when a grapple point enters the search range it will add it to the list and change the sprite 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<GrapplePoint>(out GrapplePoint grapplePoint);
        if (grapplePoint != null)
        {
            if (!grapplePointsInRange.Contains(grapplePoint))
            {
                grapplePointsInRange.Add(grapplePoint);
                grapplePoint.SetGrapplePointInRange(true);
            }
            
        }
    }
    // when the grapple point exits the search range it will remove it from the list and change the sprite back to normal
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.TryGetComponent<GrapplePoint>(out GrapplePoint grapplePoint);
        if (grapplePoint != null)
        {
            grapplePointsInRange.Remove(grapplePoint);
            grapplePoint.SetGrapplePointInRange(false);
        }
    }
    // find the closest grapple point to the mouse and set it as the closest grapple point
    void findClosestToTheMouse()
    {
        // if the list is empty return
        if (grapplePointsInRange.Count == 0)
        {
            closestGrapplePoint = null;
            return;
        }
        // get the mouse position in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // find the closest grapple point to the mouse
        float closestDistance = Mathf.Infinity;

       
        GrapplePoint closestPoint = null;

        // loop through the list of grapple points in range and calculate the closest one to the mouse
        foreach (GrapplePoint grapplePoint in grapplePointsInRange)
        {
            float distance = Vector2.Distance(mousePos, grapplePoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = grapplePoint;
            }
        }
        // finally set the closest point calulated as the closest grapple point
        closestGrapplePoint = closestPoint;
    }
}

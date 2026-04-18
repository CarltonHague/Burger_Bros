using UnityEngine;

public class MovingDeathZone : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float resetOffset = 5;
    private float heightOfShape;
    private void Awake()
    {
        // get the height of the death zone's shape
        heightOfShape = GetComponent<Collider2D>().bounds.size.y;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void resetToCheckpoint(Transform checkpoint)
    {  
        // set the death zone's position to the bottom of the camera 
        transform.position = new Vector3(transform.position.x, checkpoint.position.y - (heightOfShape/ 2f) - resetOffset, transform.position.z);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the death zone upwards using the moveSpeed variable
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}

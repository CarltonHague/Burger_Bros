using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with the bounce pad
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                //calculate the bounce vector 
                Vector2 bounceVector = new Vector2(playerRb.linearVelocityX, bounceForce);
                //reset the player's vertical velocity to 0 before applying the bounce force to ensure a consistent bounce height regardless of the player's current vertical speed
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocityX, 0f); 
                playerRb.AddForce(bounceVector, ForceMode2D.Impulse);
            }
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

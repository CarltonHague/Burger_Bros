using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            remove();
        }
    }

    private void remove()
    {
        Destroy(gameObject); 
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 dir = Vector2.right;
    private void Update()
    {
        transform.position += dir * Time.deltaTime * speed;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Deflection")
        {
            deflected();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Deflection")
        {
            deflected();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void deflected()
    {
        dir.x *= -1;
        dir.y *= -1;
        this.gameObject.tag = "Enemy Projectile";
    }
}

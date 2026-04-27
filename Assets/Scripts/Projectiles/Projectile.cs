using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
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
        
        this.transform.eulerAngles += new Vector3(0, 0, 180);
        this.gameObject.tag = "Enemy Projectile";
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
}

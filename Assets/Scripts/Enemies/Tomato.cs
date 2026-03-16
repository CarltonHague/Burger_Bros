using UnityEngine;

public class Tomato : MonoBehaviour
{
    public GameObject player;
    public GameObject shootPoint;
    public Projectile bulletPrefab;
    public Collectible ketchupDrop;
    public float speed;
    private float distance;
    private Vector2 direction;
    public float aggroDistance;
    public float shootingDistance;
    private float shootingAngle = 0;
    public float shootingSpeed = 4;
    private float cooldown = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        

        if (distance < shootingDistance)
        {
            //calculate aim angle
            direction = player.transform.position - transform.position;
            direction.Normalize();
            shootingAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shootPoint.transform.rotation = Quaternion.Euler(Vector3.forward * shootingAngle);//physically aim gun
            shoot();
        }
        else if (distance < aggroDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);//move enemy
        }
    }


    private void shoot()
    {
        if (cooldown > shootingSpeed)
        {
            cooldown = 0;
            Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        }
        else
        {
            cooldown += Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerFeet" || other.gameObject.tag == "Player Projectile")
        {
            killMeNow();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerFeet" || other.gameObject.tag == "Player Projectile")
        {
            killMeNow();
        }
    }

    private void killMeNow()
    {
        Instantiate(ketchupDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

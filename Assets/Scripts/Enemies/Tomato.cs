using UnityEngine;

public class Tomato : MonoBehaviour
{
    //other objects and prefabs
    public GameObject player;
    public GameObject shootPoint;
    public Projectile bulletPrefab;
    public Collectible ketchupDrop;

    //patroll
    public float speed;
    public float patrolRange = 2f;
    private float posOnPatrolRange = 0f;
    private Vector3 patrolDirection = Vector2.right;

    //shooting
    private float distance;
    private Vector2 direction;
    public float aggroDistance;
    public float shootingDistance;
    private float shootingAngle = 0;
    public float secBetweenShots = 3;
    private float cooldown = 0;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); //calculate distance from player
        if (distance < shootingDistance)    //want to shoot
        {
            //calculate aim angle
            direction = player.transform.position - transform.position;
            direction.Normalize();
            shootingAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shootPoint.transform.rotation = Quaternion.Euler(Vector3.forward * shootingAngle);//physically aim gun
            shoot();
        }
        else if (distance < aggroDistance)   //want to be aggro
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);//move enemy
        }
        else   //default  patrol
        {
            if (posOnPatrolRange > patrolRange || posOnPatrolRange < 0)  //if above or below range
            {
                patrolDirection.x *= -1;  //flip direvction
            }
            transform.position += patrolDirection * speed * Time.deltaTime;  //move
            posOnPatrolRange += patrolDirection.x * speed * Time.deltaTime;  //keep track of relative to range pos

        }
    }


    private void shoot()
    {
        if (cooldown > secBetweenShots)
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
        if (other.gameObject.tag == "Player Projectile")
        {
            killMeNow();
        }
        else if (other.gameObject.tag == "PlayerFeet")
        {
            if (other.transform.position.y > transform.position.y)
            {
                killMeNow();
            }
        }
        else if (other.gameObject.tag == "DeathZone")
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

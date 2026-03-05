using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Shoot : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform LaunchOffset;
    [SerializeField] bool hasGun = true;
    [SerializeField] int maxBullets = 10;
    [SerializeField] float reloadSpeed = 2f;
    private int amountOfBullets = 0;




    private void reloadGun()
    {
        if (hasGun && amountOfBullets < maxBullets)
        {
            amountOfBullets += 1;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(reloadGun), reloadSpeed, reloadSpeed);
    }

    public void shoot(InputAction.CallbackContext context)
    {
        if (hasGun && amountOfBullets > 0)
        {
            amountOfBullets -= 1;
            Instantiate(projectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

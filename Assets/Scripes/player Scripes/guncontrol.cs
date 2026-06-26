using UnityEngine;

public class guncontrol : MonoBehaviour
{
    public static bool isFiring;
    public bullet bullet1;
    public Transform firePoint;
    public float bulletSpeed = 20;
    public float fireRate = 0.5f;
    private float timeToFire = 0;
    public float reloadTime = 3;
    
    public float ammo = 4;
    // Update is called once per frame


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( ammo >= 4)
        {
            ammo = 4;
        }
        if (ammo < 4)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                ammo += 1;
                reloadTime = 3;
            }
        }
        if (isFiring){
            timeToFire -= Time.deltaTime;
        if (timeToFire <= 0 && ammo > 0)
        {
            bullet1.bulletLifeTime = 2;
            timeToFire = fireRate;
            bullet newbullet = Instantiate(bullet1, firePoint.position, firePoint.rotation) as bullet;
            ammo -= 1;
            newbullet.speed = bulletSpeed;
        }
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
    
}

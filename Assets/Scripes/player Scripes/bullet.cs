using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20;
    public float bulletLifeTime = 2;
    public float bulletDamage = 1;
    public float bulletKnockback = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
        

    }
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "EnemyS")
        {
        warriorscripe enemy = other.gameObject.GetComponent<warriorscripe>();
        Destroy(gameObject);
        enemy.takedamage(bulletDamage);
        }
        if (other.gameObject.tag == "Level")
        {
            Destroy(gameObject);
        }

    }
}

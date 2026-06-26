using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public RoomController room;

    public float health = 5;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (room != null)
        {
            room.EnemyKilled(gameObject);
        }

        Destroy(gameObject);
    }
}

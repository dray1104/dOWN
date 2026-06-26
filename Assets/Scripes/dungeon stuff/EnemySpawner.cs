using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public bool spawnOnRoomEnter = true;

    private bool hasSpawned = false;

    private RoomController room;

    void Awake()
    {
        room = GetComponentInParent<RoomController>();
    }

    public void SpawnEnemy()
    {
        if (hasSpawned)
            return;

        hasSpawned = true;

        GameObject enemy =
            Instantiate(
                enemyPrefab,
                transform.position,
                Quaternion.identity);

        if (room != null)
        {
            room.enemies.Add(enemy);

            Enemy enemyScript = enemy.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                enemyScript.room = room;
            }
        }
    }
}
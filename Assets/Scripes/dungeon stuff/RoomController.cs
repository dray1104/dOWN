using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Room Information")]
    public Vector2Int roomGridPosition;

    public bool isStartRoom;
    public bool isBossRoom;
    public bool isShopRoom;
    public bool isItemRoom;

    [Header("Room State")]
    public bool playerVisited = false;
    public bool roomCleared = false;

    [Header("References")]
    public List<GameObject> enemies = new List<GameObject>();

    public DoorController upDoor;
    public DoorController downDoor;
    public DoorController leftDoor;
    public DoorController rightDoor;

    public void PlayerEntered()
    {
        if (!playerVisited)
        {
            Debug.Log("Entered Room: " + roomGridPosition);
            playerVisited = true;

            ActivateEnemies();
        }
         

        LockDoors();
    }

    public void EnemyKilled(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            roomCleared = true;
            UnlockDoors();
        }
    }

    void ActivateEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                enemy.SetActive(true);
        }
    }

    void LockDoors()
    {
        if (!roomCleared)
        {
            if (upDoor) upDoor.LockDoor();
            if (downDoor) downDoor.LockDoor();
            if (leftDoor) leftDoor.LockDoor();
            if (rightDoor) rightDoor.LockDoor();
        }
    }

    void UnlockDoors()
    {
        if (upDoor) upDoor.UnlockDoor();
        if (downDoor) downDoor.UnlockDoor();
        if (leftDoor) leftDoor.UnlockDoor();
        if (rightDoor) rightDoor.UnlockDoor();
    }
    public DoorController GetDoor(DoorDirection direction)
    {
        DoorController[] doors = GetComponentsInChildren<DoorController>();

        foreach (DoorController door in doors)
        {
            if (door.direction == direction)
            {
                return door;
            }
        }

        return null;
    }
}

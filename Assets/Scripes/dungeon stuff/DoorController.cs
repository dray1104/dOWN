using UnityEngine;

public class DoorController : MonoBehaviour
{
    public DoorDirection direction;

    [HideInInspector]
    public RoomController currentRoom;

    [HideInInspector]
    public DoorController connectedDoor;

    public bool locked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("player"))
            return;

        if (locked)
            return;

        if (connectedDoor == null)
            return;

        Vector3 spawnPos =
            connectedDoor.transform.position +
            connectedDoor.transform.forward * 2f;

        other.transform.position = spawnPos;

        Camera.main.transform.position = new Vector3(
            currentRoom.transform.position.x,
            Camera.main.transform.position.y,
            currentRoom.transform.position.z);
    }

    public void LockDoor()
    {
        locked = true;
    }

    public void UnlockDoor()
    {
        locked = false;
    }
}
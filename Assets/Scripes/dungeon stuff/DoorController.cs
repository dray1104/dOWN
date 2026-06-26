using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool locked = false;

    public void LockDoor()
    {
        locked = true;

        // We'll animate the door later
    }

    public void UnlockDoor()
    {
        locked = false;

        // We'll animate the door later
    }
}
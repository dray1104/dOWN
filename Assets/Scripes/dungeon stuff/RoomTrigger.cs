using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomTrigger : MonoBehaviour
{
    private RoomController room;

    private void Awake()
    {
        room = GetComponentInParent<RoomController>();

        BoxCollider box = GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("player"))
            return;

        room.PlayerEntered();
    }
}
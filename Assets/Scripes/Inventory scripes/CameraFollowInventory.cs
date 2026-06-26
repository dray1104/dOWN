using UnityEngine;

public class CameraFollowInventory : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optional: Lock the cursor to the game window
        // Find the player GameObject by tag if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player GameObject not found! Please assign it in the inspector or tag it as 'Player'.");
            }
        }
        // Reinitialize references after the scene reloads

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Follow the player's position
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    // Update is called once per frame
    }
}

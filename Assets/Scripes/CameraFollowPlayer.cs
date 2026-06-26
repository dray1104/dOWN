using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 0, -7);
    public float rotationSpeed = 1.0f; // Adjust this to control rotation sensitivity
    

    void Start()
    {
        // Optional: Lock the cursor to the game window
        Cursor.lockState = CursorLockMode.Confined;
        // Find the player GameObject by tag if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
            if (player == null)
            {
                Debug.LogError("Player GameObject not found! Please assign it in the inspector or tag it as 'player'.");
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

            // Get mouse movement
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the camera slightly based on mouse movement
            transform.RotateAround(player.transform.position, Vector3.up, mouseX * rotationSpeed);
            transform.RotateAround(player.transform.position, transform.right, -mouseY * rotationSpeed);
        }
    }

    

   
    // Coroutine to reassign the player after the scene reloads
    private IEnumerator ReassignPlayer()
    {
        yield return null; // Wait for one frame to ensure the scene is fully loaded

        player = GameObject.FindGameObjectWithTag("player");
        if (player == null)
        {
            Debug.LogError("Player not found after scene reload!");
        }
    }

    
}
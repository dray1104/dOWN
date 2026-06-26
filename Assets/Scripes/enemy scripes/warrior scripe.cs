using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
public class warriorscripe : MonoBehaviour
{
     public float speed = 3f; // Speed of the enemy
    public Rigidbody enemyRb; // Reference to the Rigidbody component
    public GameObject player;
    public bool hit = false;
    public float health = 2; 
    public float attackRange = 8f; // Range within which the enemy can attack the player
    // Flag to check if the enemy has hit the player
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Renderer[] rend;
    public Color flashColor = Color.red; // Color to flash when hit
    public AudioClip diesound;
    private AudioSource PlayerAudio;
    private Color[] origColors;
    public float flashTime = 0.15f;
    private Animator anim;
    private int following;




     // Reference to the player object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>(); 
        agent = GetComponent<NavMeshAgent>();
        origColors = new Color[rend.Length];
        player = GameObject.FindGameObjectWithTag("player"); // Find the player object by tag
        following = Animator.StringToHash("following");
        anim = GetComponent<Animator>();
        // Find the kill count script in the scene (not just on this object)
        for (int i = 0; i < rend.Length; i++)
        {
            origColors[i] = rend[i].material.color;
        } // Store the original color
         // Get the NavMeshAgent component attached to the enemy
         PlayerAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0.0f) // Check if the enemy's health is less than or equal to 0
        {
            // Enemy is already dead, do nothing (handled in takedamage)
            return;
        }
        if (transform.position.y < -10) // Check if the enemy is below a certain height
        {
            Destroy(gameObject); // Destroy the enemy object
        }
        // Rotate the enemy to face the player
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange) // Check if the enemy is close to the player
        {
        
        attackRange = 16f;
        Vector3 lookDirection = (player.transform.position - transform.position);
        agent.destination = player.transform.position; // Move the enemy towards the player
        transform.LookAt(player.transform.position); 
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y -90, 0); // Keep the enemy upright
        anim.SetBool("following", true);
        }
        else
        {
        anim.SetBool("following", false); // Reset the animation when not following
        }
    }

 public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "player" && hit == false)
        {
            hit = true; // Set the hit flag to true
            StartCoroutine(hitcooldown()); // Start the cooldown coroutine
            playermovement playerMovement = other.gameObject.GetComponent<playermovement>();
            playerMovement.health -= 1;
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            Vector3 awayfromenemy = transform.position - other.gameObject.transform.position;
            
            awayFromPlayer.y = 0; // Set y component to 0 to keep the enemy on the ground
            awayfromenemy.y = 0; // Set y component to 0 to keep the player on the ground
            enemyRb.AddForce(awayFromPlayer * -5, ForceMode.Impulse);
            playerMovement.rb.AddForce(awayfromenemy * -5, ForceMode.Impulse); // Apply force to the player in the opposite direction
        }
        if (other.gameObject.tag == "Sword")

        {
            takedamage(1.0f);
            playermovement playerMovement = other.gameObject.GetComponent<playermovement>();
            Vector3 awayFromPlayer = playerMovement.gameObject.transform.position - transform.position;
            awayFromPlayer.Normalize(); // Normalize the direction vector
            
            // Apply knockback force
            enemyRb.AddForce(awayFromPlayer * 25f, ForceMode.Impulse);

        }
        
    }
    IEnumerator hitcooldown() 
    {
        yield return new WaitForSeconds(1); 
        hit = false; 
    }
    private IEnumerator doflash()
{
    // Change the color of all renderers to the flash color
    foreach (Renderer renderer in rend)
    {
        renderer.material.color = flashColor;
    }
    yield return new WaitForSeconds(flashTime); // Wait for the specified flash time

    // Change the color of all renderers back to their original colors
    for (int i = 0; i < rend.Length; i++)
    {
        rend[i].material.color = origColors[i];
    }
}
    public void flashStart(){
        StopCoroutine(doflash()); // Stop any existing flash coroutines
        StartCoroutine(doflash()); // Start the flash coroutine
    }
    public void takedamage(float damage)
    {
        health -= damage;
        flashStart();
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = diesound;
        audioSource.volume = 1f; // Set volume
        audioSource.Play();

        Destroy(tempAudio, diesound.length); 
        // Reduce the enemy's health by the damage amount
        if (health <= 0.0f) // Check if the enemy's health is less than or equal to 0
        {
            Debug.Log("Destroying enemy: " + gameObject.name);
            Destroy(gameObject,0.1f); // Destroy the enemy object
        }
    }
    
}

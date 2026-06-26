using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class slash : MonoBehaviour
{
    public playermovement playerMovement; // Reference to the player movement script
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         playerMovement = GetComponent<playermovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyS")
        {
            warriorscripe enemyS = other.gameObject.GetComponent<warriorscripe>();
            if (enemyS != null && enemyS.enemyRb != null)
            {
                enemyS.takedamage(1.0f); // Call the takedamage method on the enemy script

                // Calculate the knockback direction
                Vector3 awayFromPlayer = enemyS.transform.position - transform.position;
                awayFromPlayer.y = 0; // Keep the force horizontal
                awayFromPlayer.Normalize(); // Normalize the direction vector

                // Apply knockback force
                enemyS.enemyRb.AddForce(awayFromPlayer * 20f, ForceMode.Impulse);

                Debug.Log("Hit enemy and applied knockback");
            }
        }
        
        
    }
}


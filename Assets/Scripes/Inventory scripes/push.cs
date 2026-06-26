using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Collections;



public class push : MonoBehaviour
{
    public TimerState timerState = new TimerState();
    public float timeD = 0f;
    private bool isHolding = false;
    public float forceMultiplier = 10f; // scales hold time to force
    private float originalSpeed = 0f;
    public float dashCharge = 2f;
    private bool airBorne;
    private bool hasLaunched = false;
    private pauseMenu pauseMenuScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<playermovement>();
       pauseMenuScript = FindAnyObjectByType<pauseMenu>();

        // Try to get the followplayer script from the main camera (common case)
        if (Camera.main != null)
        {
            cameraScript = Camera.main;
        }
        // Fallback: search the scene for any followplayer component
        if (cameraScript == null)
        {
            cameraScript = Camera.main;
        }

        // Store original speed to restore it reliably
        if (playerMovement != null)
            originalSpeed = playerMovement.speed;
    }
    public float force = 10.0f;
    private Rigidbody rb;
    public float maxForce = 50.0f;
    private playermovement playerMovement;
    private Camera cameraScript;
    public float distance = 7f;
    public float rotationSpeed = 5f;
    public Vector3 launchDirection; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // (Moved initialization of st to here)
    // Update is called once per frame
    void Update()
    {
        // When the key is first pressed, begin holding
        
        Dash();

    }
    public void Dash()
    {
        // HOLDING SHIFT (charging dash)
        if (Input.GetKey(KeyCode.LeftShift) && !isHolding && dashCharge > 0f && !pauseMenuScript.isPaused)
        {
            isHolding = true;
            timeD = 0f;

            if (playerMovement != null)
            {
                playerMovement.speed = originalSpeed * 0.5f;
            }

            playerMovement.dashcooldown = true;
            playerMovement.anim.SetTrigger("dash");
            playerMovement.PlayerAudio.PlayOneShot(playerMovement.dashesound, 0.3f);
        }

        // WHILE HOLDING (charge builds)
        if (isHolding && Input.GetKey(KeyCode.LeftShift))
        {
            timeD += Time.deltaTime * 3f;
        }

        // RELEASE SHIFT (launch dash)
        if (isHolding && Input.GetKeyUp(KeyCode.LeftShift) && dashCharge > 0f && !pauseMenuScript.isPaused)
        {
            hasLaunched = true;
            isHolding = false;

            float appliedForce = timeD * forceMultiplier;
            appliedForce = Mathf.Clamp(appliedForce, 0f, maxForce);

            // ✅ USE CAMERA DIRECTION (FIXED)
            Vector3 launchDir = Camera.main.transform.forward;
            launchDir.y = 0f; // keep dash horizontal
            launchDir.Normalize();

            // ✅ MAKE PLAYER FACE DASH DIRECTION
            if (launchDir != Vector3.zero)
            {
                transform.forward = launchDir;
            }

            LaunchObject(appliedForce, launchDir);

            Debug.Log("Applied force: " + appliedForce);
            Debug.Log("Direction: " + launchDir);

            // Reset
            timeD = 0f;

            if (playerMovement != null)
                playerMovement.speed = originalSpeed;

            dashCharge -= 1f;

            StartCoroutine(DashReset());
        }
    }

    private void LaunchObject(float appliedForce, Vector3 launchDirection)
    {
        // Add slight upward arc
        launchDirection += Vector3.up * 0.5f;
        launchDirection.Normalize();

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(launchDirection * appliedForce, ForceMode.Impulse);

        Time.timeScale = 1f;
    }

    private IEnumerator DashReset()
    {
        yield return new WaitForSeconds(0.10f);

        if (playerMovement != null)
            playerMovement.speed = originalSpeed;


        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1.0f); // adjust cooldown time

        if (playerMovement != null)
            playerMovement.dashcooldown = false;
    }

    
}
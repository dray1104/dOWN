using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class playermovement : MonoBehaviour
{

    [Header("Player Movement Settings")]
    public float speed = 5;
    public Rigidbody rb;
    public Vector3 input;
    [Header("Player dash Settings")]
    public bool dashcooldown = false;
    public float playerrotation = -90f;
    public float dashspeed = 30f;
    public int health = 4;
    public int maxhealth = 4;
    [Header("Player animation Settings")]
    public Animator anim;
    private int slashC;
    private int dashing;
    [Header("Player Audio Settings")]
    public AudioClip slashsound;
    public AudioClip dashesound;
    public AudioSource PlayerAudio;
    [Header("Player Camera Settings")]
    public Camera gameCamera;
    [Header("Player UI Settings")]
    public GameObject pauseMenu;
    private pauseMenu pauseMenuScript;
    [Header("Player Ability1 Settings")]
    private bool SlashCooldown = true;
    public bool abilityBeingUsed1 = false;
    public Slot abilitySlot;
    [Header("Player Ability2 Settings")]
    public float timeD = 0f;
    private bool isHolding = false;
    public float forceMultiplier = 10f; // scales hold time to force
    private float originalSpeed = 5f;
    public float dashCharge = 2f;
    public float DashCooldowns = 1f;
    private bool hasLaunched = false;
    public float force = 10.0f;
    public float maxForce = 50.0f;
    public float distance = 7f;
    public float rotationSpeed = 5f;
    private Vector3 launchDirection; 
    private bool abilityBeingUsed2 = false;
    public Slot abilitySlot2;
    public Slot abilitySlot3;
    public Slot abilitySlot4;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        slashC = Animator.StringToHash("slashing");
        dashing = Animator.StringToHash("dashing");
        PlayerAudio = GetComponent<AudioSource>();
        pauseMenuScript = pauseMenu.GetComponent<pauseMenu>();

        if (gameCamera == null)
        {
            gameCamera = Camera.main;
            if (gameCamera == null)
            {
                Debug.LogError("Main Camera not found! Ensure the camera has the 'MainCamera' tag.");
            }
        }
    }
    void Update()
    {
        
        
        Slash();
        dash();
        lookAtMouse();
        Movement();
        
        

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Movement()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
        input = new Vector3(input.x, 0f, input.z);
        input = input.normalized;
         transform.Translate(input * speed * Time.deltaTime, Space.World);
        
    }
     public void lookAtMouse()
    {
        Ray cameraRay = gameCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 direction = pointToLook - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction);
                transform.rotation = targetRotation * Quaternion.Euler(0, 0, 0);
            }

            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);
        }
        else
        {
            Debug.LogWarning("Ray did not hit the ground plane.");
        }
    }  
        public void Slash()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            anim.SetBool(slashC, true);
            
            if (SlashCooldown == true)
            {
                PlayerAudio.PlayOneShot(slashsound, 1f);
                StartCoroutine(slashSoundCooldown());
                SlashCooldown = false;
                abilityBeingUsed1 = true;
            }
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool(slashC, false);
        }
    }
     private IEnumerator slashSoundCooldown()
    {
        yield return new WaitForSeconds(0.8f);
        SlashCooldown = true;
        abilityBeingUsed1 = false;
    }
    public void dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashcooldown == false && input != Vector3.zero)
        {
            dashcooldown = true;
            anim.SetTrigger("dash");
            StartCoroutine(PerformDash());
            PlayerAudio.PlayOneShot(dashesound, 0.3f);
        }
    }

    private IEnumerator PerformDash()
    {
        float originalSpeed = speed;
        speed = dashspeed; // Temporarily increase speed
        rb.linearVelocity = input * dashspeed; // Apply dash force
        yield return new WaitForSeconds(0.10f); // Dash duration
        speed = originalSpeed; // Reset speed
        rb.linearVelocity = Vector3.zero; // Stop movement after dash
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(DashCooldowns);
        dashcooldown = false;
    }
       
}

